using E.Application.Models;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.Commands;

public class ApplyCouponCommand : IRequest<OperationResult<Coupon>>
{
    public Guid ProductId { get; set; }
    public Guid CouponId { get; set; }
    public decimal CartTotal { get; set; }
}