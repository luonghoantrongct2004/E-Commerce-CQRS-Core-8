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

namespace E.Application.Identity.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OperationResult<IdentityUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<BasicUser> _userManager;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher,
        AppDbContext appDbContext, UserManager<BasicUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _appDbContext = appDbContext;
        _userManager = userManager;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<IdentityUserDto>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var identityUser = await _appDbContext.Users.
                FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (identityUser is null)
            {
                result.AddError(ErrorCode.IdentityUserDoesNotExist,
                    IdentityErrorMessages.NonExistentIdentityUser);
                return result;
            }
            var userProfile = await _unitOfWork.Users.
                FirstOrDefaultAsync(up => up.IdentityId == request.UserId.ToString());
            if (userProfile is null)
            {
                result.AddError(ErrorCode.NotFound, IdentityErrorMessages.NonExistentIdentityUser);
                return result;
            }
            if (identityUser.Id != request.RequestorGuid)
            {
                result.AddError(ErrorCode.UnauthorizedAccountRemoval,
                    IdentityErrorMessages.UnauthorizedAccountRemoval);

                return result;
            }

            var hashedPassword = _userManager.PasswordHasher.HashPassword(identityUser, request.Password);
            identityUser.PasswordHash = hashedPassword;

            var basicUser = new BasicUser();
            basicUser.UpdateBasicInfo(
                username: request.Username,
                password: hashedPassword,
                fullName: request.FullName,
                avatar: request.Avatar,
                address: request.Address,
                currentCity: request.CurrentCity
            );
            userProfile.UpdateBasicUser(basicUser);

            var userEvent = new UserUpdateEvent(basicUser.UserName, hashedPassword,
                basicUser.FullName, basicUser.Email, basicUser.Avatar, basicUser.Address,
                basicUser.CurrentCity);
            await _eventPublisher.PublishAsync(userEvent);

            result.Payload = new IdentityUserDto
            {
                Email = identityUser.Email,
                FullName = userProfile.BasicInfo.FullName,
                Avatar = userProfile.BasicInfo.Avatar,
                Address = userProfile.BasicInfo.Address,
                CurrentCity = userProfile.BasicInfo.CurrentCity
            }; ;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(ex.Message);
        }
        return result;
    }
}