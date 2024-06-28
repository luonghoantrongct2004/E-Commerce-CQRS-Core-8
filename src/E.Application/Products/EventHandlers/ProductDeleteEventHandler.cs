using E.DAL.UoW;
using E.Domain.Entities.Products;
using E.Domain.Entities.Products.Events;
using MediatR;

namespace E.Application.Products.EventHandlers;

public class ProductDeleteEventHandler : INotificationHandler<ProductRemoveEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ProductDeleteEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }
    public async Task Handle(ProductRemoveEvent notification, CancellationToken cancellationToken)
    {
        await _readUnitOfWork.Products.RemoveAsync(notification.ProductId);
    }
}