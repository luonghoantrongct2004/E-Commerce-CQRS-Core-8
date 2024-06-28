using E.Application.Brands;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Products.Events;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Products;
using MediatR;
using static System.Net.Mime.MediaTypeNames;

namespace E.Application.Products.EventHandlers;

public class ProductUpdateEventHandler : INotificationHandler<ProductUpdateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public ProductUpdateEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(ProductUpdateEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Brand>();
        var existingProduct = await _readUnitOfWork.Products.FirstOrDefaultAsync(
            b => b.Id == notification.Id);
        if (existingProduct != null)
        {
            existingProduct.Id = notification.Id;
            existingProduct.ProductName = notification.ProductName;
            existingProduct.Description = notification.Description;
            existingProduct.Price = notification.Price;
            existingProduct.Images = notification.Images;
            existingProduct.CategoryId = notification.CategoryId;
            existingProduct.BrandId = notification.BrandId;
            existingProduct.StockQuantity = notification.StockQuantity;
            existingProduct.Discount = notification.Discount;

            await _readUnitOfWork.Products.UpdateAsync(existingProduct.Id, existingProduct);
        }
        else
        {
            result.AddError(ErrorCode.NotFound,
                   string.Format(ProductErrorMessage.ProductNotFound, notification.Id));
        }
    }
}