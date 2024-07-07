using E.Domain.Entities;
using MediatR;

namespace E.Application.Coupons.Events;

public class UpdateCouponEvent : BaseEntity, INotification
{
    public string CouponCode { get; set; }

    public decimal DiscountAmount { get; set; }

    public int MinAmount { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int UsageLimit { get; set; }

    public UpdateCouponEvent(Guid id,string couponCode, decimal discountAmount,
        int minAmount, DateTime createdDate, DateTime expirationDate, int usageLimit)
    {
        Id = id;
        CouponCode = couponCode;
        DiscountAmount = discountAmount;
        MinAmount = minAmount;
        CreatedDate = createdDate;
        ExpirationDate = expirationDate;
        UsageLimit = usageLimit;
    }
}