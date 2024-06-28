using E.Application.Brands.Queries;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.QueryHandlers;

public class GetBrandsQueryHandler
    : IRequestHandler<GetBrandsQuery, OperationResult<IEnumerable<Brand>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetBrandsQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<IEnumerable<Brand>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<IEnumerable<Brand>>();
        result.Payload = await _readUnitOfWork.Brands.GetAllAsync();
        return result;
    }
}