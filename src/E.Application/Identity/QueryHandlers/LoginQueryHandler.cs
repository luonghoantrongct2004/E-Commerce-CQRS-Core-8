using AutoMapper;
using E.Application.Enums;
using E.Application.Identity.Queries;
using E.Application.Models;
using E.Application.Services;
using E.DAL.UoW;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E.Application.Identity.QueryHandlers;

public class LoginQueryHandler : IRequestHandler<LoginQuery, OperationResult<IdentityUserDto>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly UserManager<BasicUser> _userManager;
    private readonly IdentityService _identityService;
    private readonly IMapper _mapper;
    private OperationResult<IdentityUserDto> _result = new();

    public LoginQueryHandler(IReadUnitOfWork readUnitOfWork, UserManager<BasicUser> userManager,
        IdentityService identityService, IMapper mapper)
    {
        _readUnitOfWork = readUnitOfWork;
        _userManager = userManager;
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var identityUser = await ValidateAndGetIdentityAsync(request);

        if (_result.IsError) return _result;

        var userProfile = await _readUnitOfWork.Users.FirstOrDefaultAsync(u => u.Id == identityUser.Id);
        _result.Payload = _mapper.Map<IdentityUserDto>(userProfile);
        _result.Payload.Email = identityUser.Email;
        _result.Payload.Token = GetJwtString(identityUser);

        return _result;
    }
    private async Task<BasicUser> ValidateAndGetIdentityAsync(LoginQuery request)
    {
        var identityUser = await _userManager.FindByEmailAsync(request.UserName);
        if (identityUser is null) _result.AddError(ErrorCode.IdentityUserDoesNotExist,
            IdentityErrorMessages.NonExistentIdentityUser);
        var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);
        if (!validPassword) _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessages.IncorrectPassword);
        return identityUser;
    }
    private string GetJwtString(BasicUser identityUser)
    {
        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
            new Claim("IdentityId", identityUser.Id.ToString()),
        });
        var token = _identityService.CreateSecurityToken(claimsIdentity);
        return _identityService.WriteToken(token);
    }
}