using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.UserServices;
using E.DAL.UoW;
using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Identity.EventHandlers;

public class DisableUserCommandEventHandler : INotificationHandler<UserDisableEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly UserService _userService;

    public DisableUserCommandEventHandler(IReadUnitOfWork readUnitOfWork,
        UserService userService)
    {
        _readUnitOfWork = readUnitOfWork;
        _userService = userService;
    }

    public async Task Handle(UserDisableEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserMongo>();
        try
        {
            var existingEntity = await _readUnitOfWork.Users.FirstOrDefaultAsync(
                b => b.Id == notification.IdentityUserId);

            if (existingEntity != null)
            {
                _userService.DisableUserMongo(existingEntity);
                await _readUnitOfWork.Users.UpdateAsync(existingEntity.Id, existingEntity);
            }
            else
            {
                result.AddError(ErrorCode.NotFound,
                string.Format(UserErrorMessage.UserNotFound(notification.IdentityUserId)));
            }
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}