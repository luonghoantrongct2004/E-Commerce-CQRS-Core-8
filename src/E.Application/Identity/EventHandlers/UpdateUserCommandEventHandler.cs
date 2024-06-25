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

        if (existingUser == null)
        {
            var user = new UserMongo
            {
                UserName = notification.Username,
                PasswordHash = notification.PasswordHash,
                FullName = notification.FullName,
                CreatedDate = notification.CreatedDate,
                Avatar = notification.Avatar,
                Address = notification.Address,
                CurrentCity = notification.CurrentCity,
            };
            await _readUnitOfWork.Users.AddAsync(user);
        }
    }
}