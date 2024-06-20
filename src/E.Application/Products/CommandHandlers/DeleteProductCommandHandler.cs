using E.Application.Enums;
using E.Application.Models;
using E.Application.Products.Commands;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using E.Domain.Entities.Products.Events;
using MediatR;

namespace E.Application.Products.CommandHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, OperationResult<Product>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<Product>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Product>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var product = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.ProductId.Equals(request.ProductId));
            if (product is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ProductErrorMessage.ProductNotFound, request.ProductId));
                return result;
            }
            if (product.ProductId != request.ProductId)
            {
                result.AddError(ErrorCode.PostDeleteNotPossible, ProductErrorMessage.ProductDeleteNotPossible);
                return result;
            }
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CompleteAsync();

            var productEvent = new ProductDeleteEvent(product.ProductId);
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