using AutoMapper;
using E.Application.Enums;
using E.Application.Identity.Commands;
using E.Application.Models;
using E.Application.Services;
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
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IdentityService _identityService;
    private OperationResult<IdentityUserDto> _result = new();
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager,
        IdentityService identityService, IMapper mapper, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _identityService = identityService;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        await ValidateIdentityDoesNotExist(request);
        if (_result.IsError) return _result;

        await _unitOfWork.BeginTransactionAsync();

        var identity = await CreateIdentityUserAsync(request);

        if (_result.IsError) return _result;

        var user = await CreateUserAsync(request, identity);
        var userEvent = new UserRegisterEvent(user.UserProfileId, user.IdentityId,
            user.BasicInfo, user.DateCreated, user.LastModified);
        await _eventPublisher.PublishAsync(userEvent);

        await _unitOfWork.CommitAsync();

        _result.Payload = _mapper.Map<IdentityUserDto>(user);
        _result.Payload.UserName = identity.UserName;
        _result.Payload.Token = GetJwtString(identity, user);
        return _result;
    }

    private async Task ValidateIdentityDoesNotExist(RegisterUserCommand request)
    {
        var existingIdentity = await _userManager.FindByEmailAsync(request.Username);

        if (existingIdentity != null)
            _result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessages.IdentityUserAlreadyExists);
    }

    private async Task<IdentityUser> CreateIdentityUserAsync(RegisterUserCommand request)
    {
        var identity = new IdentityUser
        {
            Email = request.Username,
            UserName = request.Username
        };
        var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
        if (!createdIdentity.Succeeded)
        {
            await _unitOfWork.BeginTransactionAsync();
            foreach (var identityError in createdIdentity.Errors)
            {
                _result.AddError(ErrorCode.IdentityCreationFailed, identityError.Description);
            }
        }
        return identity;
    }

    private async Task<User> CreateUserAsync(RegisterUserCommand request,
        IdentityUser identity)
    {
        try
        {
            var profileInfo = BasicUser.CreateBasicInfo(request.FullName, request.Email,
                request.Avatar, request.Address, request.CurrentCity);
            var user = User.CreateUser(identity.Id, profileInfo);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    private string GetJwtString(IdentityUser identity, User user)
    {
        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identity.Email),
            new Claim("IdentityId", identity.Id),
            new Claim("UserProfileId", user.UserProfileId.ToString())
        });
        var token = _identityService.CreateSecurityToken(claimsIdentity);
        return _identityService.WriteToken(token);
    }
}