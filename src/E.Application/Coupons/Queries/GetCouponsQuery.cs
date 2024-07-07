using E.Application.Models;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.Queries;

public class GetCouponsQuery : IRequest<OperationResult<IEnumerable<Coupon>>>
{
}