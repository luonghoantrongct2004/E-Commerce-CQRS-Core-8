using E.Application.Enums;
using E.Application.Models;
using E.Application.Products.Queries;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Products.QueryHandlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProduct, OperationResult<Product>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetProductByIdQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<Product>> Handle(GetProduct request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Product>();
        var product = await _readUnitOfWork.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId);
        if (product is null)
        {
            result.AddError(ErrorCode.NotFound,
               ProductErrorMessage.ProductNotFound(request.ProductId));
            return result;
        }
        if (!product.IsActive)
        {
            result.AddError(ErrorCode.ValidationError,
                ProductErrorMessage.ProductStoppedWorking(product.ProductName));
            return result;
        }
        result.Payload = product;
        return result;
    }
}