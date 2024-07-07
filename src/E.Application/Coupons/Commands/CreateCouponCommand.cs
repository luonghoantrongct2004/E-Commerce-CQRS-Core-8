using E.Application.Models;
using E.Domain.Entities.Coupons;
using E.Domain.Enum;
using MediatR;

namespace E.Application.Coupons.Commands;

public class CreateCouponCommand : IRequest<OperationResult<Coupon>>
{
    public string CouponCode { get; set; }

    public decimal DiscountAmount { get; set; }

    public int MinAmount { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int UsageLimit { get; set; }
    public decimal DiscountPercentage { get; set; }
    public CouponType Type { get; set; }
}