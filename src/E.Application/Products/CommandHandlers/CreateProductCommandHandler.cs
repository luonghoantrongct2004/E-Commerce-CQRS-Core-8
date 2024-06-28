using E.Application.Models;
using E.Application.Products.Commands;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using E.Domain.Entities.Products.Events;
using MediatR;

namespace E.Application.Products.CommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, OperationResult<Product>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<Product>> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Product>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var product = Product.CreateProduct(request.ProductName,request.Description,
                request.Price,request.Images,request.CategoryId,request.BrandId,
                request.StockQuantity,request.Discount);

            await _unitOfWork.Products.AddAsync(product);

            var productEvent = new ProductCreateEvent(product.Id, product.ProductName,
                product.Description, product.Price, product.Images, product.CategoryId,
                product.BrandId, product.StockQuantity, product.Discount, product.CreatedAt);
            await _eventPublisher.PublishAsync(productEvent);

            await _unitOfWork.CommitAsync();
            result.Payload = product;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}