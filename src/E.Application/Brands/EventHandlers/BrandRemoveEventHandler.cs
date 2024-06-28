using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.EventHandlers;

public class BrandRemoveEventHandler : INotificationHandler<BrandRemoveEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public BrandRemoveEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(BrandRemoveEvent notification, CancellationToken cancellationToken)
    {
        await _readUnitOfWork.Users.RemoveAsync(notification.BrandId);
    }
}