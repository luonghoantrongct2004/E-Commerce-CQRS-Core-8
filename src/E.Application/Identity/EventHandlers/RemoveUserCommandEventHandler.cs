using E.Application.Brands;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Users;
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
        var result = new OperationResult<DomainUser>();
        var existingEntity = await _readUnitOfWork.Users.FirstOrDefaultAsync(
            b => b.Id == notification.IdentityUserId);

        if (existingEntity != null) {
            await _readUnitOfWork.Users.RemoveAsync(existingEntity.Id);
        }
        else
        {
            result.AddError(ErrorCode.NotFound,
            string.Format(UserErrorMessage.UserNotFound, notification.IdentityUserId));
        }
        
    }
}