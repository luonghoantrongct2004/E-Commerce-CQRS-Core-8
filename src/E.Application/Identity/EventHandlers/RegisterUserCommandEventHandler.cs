using E.DAL.UoW;
using E.Domain.Entities.Users;
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
        var user = new User
        {
            UserProfileId = notification.UserProfileId,
            IdentityId = notification.IdentityId,
            BasicInfo = notification.BasicInfo,
            DateCreated = notification.DateCreated,
            LastModified = notification.LastModified,
        };
        await _readUnitOfWork.Users.AddAsync(user);
    }
}