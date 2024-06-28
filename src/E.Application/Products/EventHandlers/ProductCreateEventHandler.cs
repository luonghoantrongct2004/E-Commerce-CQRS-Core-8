using E.DAL.UoW;
using E.Domain.Entities.Products;
using E.Domain.Entities.Products.Events;
using MediatR;

namespace E.Application.Products.EventHandlers;

public class ProductCreateEventHandler : INotificationHandler<ProductCreateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ProductCreateEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(ProductCreateEvent notification, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = notification.Id,
            ProductName = notification.ProductName,
            Description = notification.Description,
            Price = notification.Price,
            Images = notification.Images,
            CategoryId = notification.CategoryId,
            BrandId = notification.BrandId,
            StockQuantity = notification.StockQuantity,
            Discount = notification.Discount
        };
        await _readUnitOfWork.Products.AddAsync(product);
    }
}