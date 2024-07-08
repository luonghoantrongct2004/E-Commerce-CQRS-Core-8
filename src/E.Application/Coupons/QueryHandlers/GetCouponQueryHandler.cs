using E.Application.Coupons.Queries;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.QueryHandlers;

public class GetCouponQueryHandler
    : IRequestHandler<GetCouponQuery, OperationResult<Coupon>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetCouponQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<Coupon>> Handle(GetCouponQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Coupon>();
        var coupon = await _readUnitOfWork.Coupons.FirstOrDefaultAsync(
            b => b.Id == request.Id);
        if (coupon is null)
        {
            result.AddError(ErrorCode.NotFound,
                CouponErrorMessage.CouponNotFound);
            return result;
        }
        result.Payload = coupon;
        return result;
    }
}