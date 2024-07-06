using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.ProductServices;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using E.Domain.Entities.Products.Events;
using MediatR;

namespace E.Application.Products.EventHandlers;

public class ProductRemoveEventHandler : INotificationHandler<ProductRemoveEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly ProductService _productService;

    public ProductRemoveEventHandler(IReadUnitOfWork readUnitOfWork,
        ProductService productService)
    {
        _readUnitOfWork = readUnitOfWork;
        _productService = productService;
    }

    public async Task Handle(ProductRemoveEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Product>();
        try
        {
            var existingProduct = await _readUnitOfWork.Products.FirstOrDefaultAsync(
                b => b.Id == notification.Id);
            if (existingProduct != null)
            {
                _productService.DisableProduct(existingProduct);
                await _readUnitOfWork.Products.UpdateAsync(existingProduct.Id, existingProduct);
            }
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}