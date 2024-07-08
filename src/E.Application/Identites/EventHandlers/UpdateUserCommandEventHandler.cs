using E.Application.Enums;
using E.Application.Identity.Events;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Identity.EventHandlers;

public class UpdateUserCommandEventHandler : INotificationHandler<UserUpdateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public UpdateUserCommandEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(UserUpdateEvent notification,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserMongo>();
        try
        {
            var existingUser = await _readUnitOfWork.Users.FirstOrDefaultAsync(
                u => u.Id == notification.UserId);

            if (existingUser != null)
            {
                existingUser.UserName = notification.Username;
                existingUser.PasswordHash = notification.PasswordHash;
                existingUser.FullName = notification.FullName;
                existingUser.CreatedDate = notification.CreatedDate;
                existingUser.Avatar = notification.Avatar;
                existingUser.Address = notification.Address;
                existingUser.CurrentCity = notification.CurrentCity;

                await _readUnitOfWork.Users.UpdateAsync(existingUser.Id, existingUser);
            }
            else
            {
                result.AddError(ErrorCode.NotFound,
                string.Format(UserErrorMessage.UserNotFound(notification.UserId)));
            }
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}