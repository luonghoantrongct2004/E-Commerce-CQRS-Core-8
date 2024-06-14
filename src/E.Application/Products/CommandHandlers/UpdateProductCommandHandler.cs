using E.Application.Enums;
using E.Application.Models;
using E.Application.Products.Commands;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using E.Domain.Entities.Products.Events;
using MediatR;

namespace E.Application.Products.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, OperationResult<Product>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<Product>> Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Product>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var product = await _unitOfWork.Products.FirstOrDefaultAsync(
                p => p.ProductId == request.ProductId);
            if (product is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ProductErrorMessage.ProductNotFound, request.ProductId));
                return result;
            }
            if (!product.ProductId.Equals(request.ProductId))
            {
                result.AddError(ErrorCode.PostDeleteNotPossible,
                    ProductErrorMessage.ProductDeleteNotPossible);
                return result;
            }
            product.UpdateProduct(
                productName: request.ProductName,
                description: request.Description,
                price: request.Price,
                images: request.Images,
                categoryId: (Guid)request.CategoryId,
                brandId: (Guid)request.BrandId,
                stockQuantity: request.StockQuantity,
                discount: (int)request.Discount
            );

            var productEvent = new ProductUpdateEvent(product.ProductId, product.ProductName,
                product.Description, product.Price, product.Images, product.CategoryId,
                product.BrandId, product.StockQuantity, product.Discount, product.CreatedAt);

            await _eventPublisher.PublishAsync(productEvent);

            await _unitOfWork.CommitAsync();
            result.Payload = product;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(ex.Message);
        }
        return result;
    }
}