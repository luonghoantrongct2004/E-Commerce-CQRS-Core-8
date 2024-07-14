using AutoMapper;
using E.Application.Identity;
using E.Application.Models;
using E.Application.Services.TokenServices;
using E.Application.Services.UserServices;
using E.Application.Token.Commands;
using E.Domain.Entities.Token.Dto;
using E.Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E.Application.Token.CommandHandlers;

public class TokenCommandHandler : IRequestHandler<TokenCommand, OperationResult<TokenDto>>
{
    private readonly UserManager<DomainUser> _userManager;
    private readonly RefreshTokenService _refreshTokenService;
    private readonly IdentityService _identityService;
    private readonly IMapper _mapper;
    private OperationResult<TokenDto> _result = new();

    public TokenCommandHandler(UserManager<DomainUser> userManager,
        RefreshTokenService refreshTokenService, IdentityService identityService,
        IMapper mapper)
    {
        _userManager = userManager;
        _refreshTokenService = refreshTokenService;
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<OperationResult<TokenDto>> Handle(TokenCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UserName);
        if (user is null)
        {
            _result.AddError(Enums.ErrorCode.NotFound,
                UserErrorMessage.TokenNotFound);
            return _result;
        }
        var savedRefreshToken = await _refreshTokenService.GetRefreshTokenAsync(user);
        if (savedRefreshToken != request.RefreshToken)
        {
            _result.AddError(Enums.ErrorCode.ValidationError,
                TokenErrorMessage.TokenInvalid);
            return _result;
        }
        var newAccessToken = GetJwtString(user);
        _result.Payload = _mapper.Map<TokenDto>(newAccessToken);
        return _result;
    }

    private string GetJwtString(DomainUser identityUser)
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