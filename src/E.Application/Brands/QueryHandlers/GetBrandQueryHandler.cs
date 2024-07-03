using E.Application.Brands.Queries;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.QueryHandlers;

public class GetBrandQueryHandler : IRequestHandler<GetBrandQuery, OperationResult<Brand>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetBrandQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<Brand>> Handle(GetBrandQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Brand>();
        var brand = await _readUnitOfWork.Brands.FirstOrDefaultAsync(
            b => b.Id == request.Id);
        if (brand is null)
        {
            result.AddError(ErrorCode.NotFound,
                BrandErrorMessage.BrandNotFound);
            return result;
        }
        result.Payload = brand;
        return result;
    }
}