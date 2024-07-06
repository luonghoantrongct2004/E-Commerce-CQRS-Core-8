using E.Application.Enums;
using E.Application.Models;
using E.Application.Products.Commands;
using E.Application.Services.ProductServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using E.Domain.Entities.Products.Events;
using MediatR;

namespace E.Application.Products.CommandHandlers;

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, OperationResult<Product>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly ProductService _productService;

    public RemoveProductCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, ProductService productService)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _productService = productService;
    }

    public async Task<OperationResult<Product>> Handle(RemoveProductCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Product>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var product = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.Id.Equals(request.Id));
            if (product is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ProductErrorMessage.ProductNotFound(request.Id)));
                return result;
            }
            if (product.Id != request.Id)
            {
                result.AddError(ErrorCode.PostDeleteNotPossible, ProductErrorMessage.ProductDeleteNotPossible);
                return result;
            }
            _productService.DisableProduct(product);
            _unitOfWork.Products.Update(product);

            var productEvent = new ProductRemoveEvent(product.Id);
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