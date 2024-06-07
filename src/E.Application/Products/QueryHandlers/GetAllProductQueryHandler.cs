using AutoMapper;
using E.Application.Models;
using E.Application.Products.Queries;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Products.QueryHandlers;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProducts, OperationResult<IEnumerable<Product>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IEnumerable<Product>>> Handle(GetAllProducts request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<IEnumerable<Product>>();
        try
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            result.Payload = products;
        }catch(Exception ex)
        {
            result.AddUnknownError(ex.Message);
        }
        return result;
    }
}