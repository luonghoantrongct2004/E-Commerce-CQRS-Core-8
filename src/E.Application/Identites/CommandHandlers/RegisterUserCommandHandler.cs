using AutoMapper;
using E.Application.Enums;
using E.Application.Identity.Commands;
using E.Application.Models;
using E.Application.Services.TokenServices;
using E.Application.Services.UserServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Dto;
using E.Domain.Entities.Users.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E.Application.Identity.CommandHandlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<IdentityUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly UserManager<DomainUser> _userManager;
    private readonly IdentityService _identityService;
    private OperationResult<IdentityUserDto> _result = new();
    private readonly IMapper _mapper;
    private readonly RefreshTokenService _refreshToken;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork,
        UserManager<DomainUser> userManager,
        IdentityService identityService, IMapper mapper,
        IEventPublisher eventPublisher, RefreshTokenService refreshToken)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _identityService = identityService;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
        _refreshToken = refreshToken;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await ValidateIdentityDoesNotExist(request);
            if (_result.IsError) return _result;

            await _unitOfWork.BeginTransactionAsync();
            var identity = await CreateIdentityUserAsync(request);
            await _unitOfWork.CompleteAsync();

            if (_result.IsError) return _result;

            var userEvent = new UserRegisterEvent(identity.Id, identity.UserName,
                identity.PasswordHash, identity.FullName, identity.CreatedDate,
                identity.Avatar, identity.Address, identity.CurrentCity);
            await _eventPublisher.PublishAsync(userEvent);

            await _unitOfWork.CommitAsync();

            var refreshToken = await _refreshToken.CreateRefreshTokenAsync(identity);

            _result.Payload = _mapper.Map<IdentityUserDto>(identity);
            _result.Payload.UserName = identity.Email;
            _result.Payload.Token = GetJwtString(identity);
            _result.Payload.RefreshToken = refreshToken;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            _result.AddUnknownError(ex.Message);
        }
        return _result;
    }

    private async Task ValidateIdentityDoesNotExist(RegisterUserCommand request)
    {
        var existingIdentity = await _userManager.FindByEmailAsync(request.Username);

        if (existingIdentity != null)
            _result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessages.IdentityUserAlreadyExists);
    }

    private async Task<DomainUser> CreateIdentityUserAsync(RegisterUserCommand request)
    {
        var identity = new DomainUser
        {
            Email = request.Username,
            UserName = request.Username,
            NormalizedEmail = request.Username.ToUpper(),
            FullName = request.FullName,
            CreatedDate = request.CreatedDate,
            Avatar = request.Avatar,
            Address = request.Address,
            CurrentCity = request.CurrentCity,
        };

        var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
        if (!createdIdentity.Succeeded)
        {
            foreach (var identityError in createdIdentity.Errors)
            {
                _result.AddError(ErrorCode.IdentityCreationFailed, identityError.Description);
            }
        }

        return identity;
    }

    private string GetJwtString(DomainUser identity)
    {
        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identity.Email),
            new Claim("IdentityId", identity.Id.ToString()),
        });
        var token = _identityService.CreateSecurityToken(claimsIdentity);
        return _identityService.WriteToken(token);
    }
}