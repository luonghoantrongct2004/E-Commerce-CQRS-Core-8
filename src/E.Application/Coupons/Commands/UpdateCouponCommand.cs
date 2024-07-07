using E.Application.Models;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.Commands;

public class UpdateCouponCommand : IRequest<OperationResult<Coupon>>
{
    public Guid Id { get; set; }
    public string CouponCode { get; set; }

    public decimal DiscountAmount { get; set; }

    public int MinAmount { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int UsageLimit { get; set; }
}