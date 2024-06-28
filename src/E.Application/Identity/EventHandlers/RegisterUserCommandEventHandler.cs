using E.DAL.UoW;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Dto;
using E.Domain.Entities.Users.Events;
using MediatR;

namespace E.Application.Identity.EventHandlers;

public class RegisterUserCommandEventHandler : INotificationHandler<UserRegisterEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public RegisterUserCommandEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(UserRegisterEvent notification, CancellationToken cancellationToken)
    {
        var existingUser = await _readUnitOfWork.Users.FirstOrDefaultAsync(u => u.Id == notification.UserId);

        if (existingUser == null)
        {
            var user = new UserMongo
            {
                Id = notification.UserId,
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
