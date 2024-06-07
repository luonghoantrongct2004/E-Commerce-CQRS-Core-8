using E.Application.Enums;
using E.Application.Models;
using E.Application.Products.Queries;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Products.QueryHandlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductById, OperationResult<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<Product>> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Product>();
        var product = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId);
        if (product is null)
        {
            result.AddError(ErrorCode.NotFound,
                string.Format(ProductErrorMessage.ProductNotFound, request.ProductId));
            return result;
        }
        result.Payload = product;
        return result;
    }
}