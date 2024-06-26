using E.DAL.UoW;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Events;
using MediatR;

namespace E.Application.Identity.EventHandlers;

public class UpdateUserCommandEventHandler : INotificationHandler<UserRegisterAndUpdateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public UpdateUserCommandEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(UserRegisterAndUpdateEvent notification, CancellationToken cancellationToken)
    {
        var existingUser = await _readUnitOfWork.Users.FirstOrDefaultAsync(u => u.Id == notification.UserId);

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
            throw new Exception($"User not match Id {notification.UserId}");
        }
    }
}