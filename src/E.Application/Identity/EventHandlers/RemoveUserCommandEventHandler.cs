using E.DAL.UoW;
using MediatR;

namespace E.Application.Identity.EventHandlers;

public class RemoveUserCommandEventHandler : INotificationHandler<UserRemoveEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public RemoveUserCommandEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }
    public async Task Handle(UserRemoveEvent notification, CancellationToken cancellationToken)
    {
        await _readUnitOfWork.Users.RemoveAsync(notification.IdentityUserId);
    }
}