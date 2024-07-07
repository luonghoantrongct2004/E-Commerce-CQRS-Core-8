using E.Application.Models;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.Queries;

public class GetCouponQuery : IRequest<OperationResult<Coupon>>
{
    public Guid Id { get; set; }
}