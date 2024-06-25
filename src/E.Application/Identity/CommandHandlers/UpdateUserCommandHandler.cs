using AutoMapper;
using E.Application.Enums;
using E.Application.Identity.Commands;
using E.Application.Models;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Dto;
using E.Domain.Entities.Users.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace E.Application.Identity.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OperationResult<IdentityUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<DomainUser> _userManager;
    private readonly IMapper _mapper;
    private OperationResult<IdentityUserDto> _result = new();

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher,
        AppDbContext appDbContext, UserManager<DomainUser> userManager, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _appDbContext = appDbContext;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var identityUser = await _appDbContext.Users.
                FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (identityUser is null)
            {
                _result.AddError(ErrorCode.IdentityUserDoesNotExist,
                    IdentityErrorMessages.NonExistentIdentityUser);
                return _result;
            }
            var userProfile = await _unitOfWork.Users.
                FirstOrDefaultAsync(up => up.Id == request.UserId);
            if (userProfile is null)
            {
                _result.AddError(ErrorCode.NotFound, IdentityErrorMessages.NonExistentIdentityUser);
                return _result;
            }
            if (identityUser.Id != request.RequestorGuid)
            {
                _result.AddError(ErrorCode.UnauthorizedAccountRemoval,
                    IdentityErrorMessages.UnauthorizedAccountRemoval);

                return _result;
            }

            var hashedPassword = _userManager.PasswordHasher.HashPassword(identityUser, request.Password);
            identityUser.PasswordHash = hashedPassword;

            var basicUser = new DomainUser();
            basicUser.UpdateBasicInfo(
                username: request.Username,
                password: hashedPassword,
                fullName: request.FullName,
                avatar: request.Avatar,
                address: request.Address,
                currentCity: request.CurrentCity
            );

            await _unitOfWork.CompleteAsync();

            var userEvent = new UserRegisterAndUpdateEvent(basicUser.Id,basicUser.UserName, hashedPassword,
                basicUser.FullName, basicUser.CreatedDate, basicUser.Avatar, basicUser.Address,basicUser.CurrentCity);
            await _eventPublisher.PublishAsync(userEvent);

            await _unitOfWork.CommitAsync();

            _result.Payload = _mapper.Map<IdentityUserDto>(basicUser);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            _result.AddUnknownError(ex.Message);
        }
        return _result;
    }
}