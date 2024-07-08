using E.Application.Coupons.Queries;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.QueryHandlers;

public class GetCouponsQueryHandler
    : IRequestHandler<GetCouponsQuery, OperationResult<IEnumerable<Coupon>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetCouponsQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<IEnumerable<Coupon>>> Handle(GetCouponsQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<IEnumerable<Coupon>>();
        result.Payload = await _readUnitOfWork.Coupons.GetAllAsync();
        return result;
    }
}