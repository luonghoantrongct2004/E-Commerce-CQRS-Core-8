using E.Domain.Entities;
using E.Domain.Enum;
using MediatR;

namespace E.Application.Coupons.Events;

public class CreateCouponEvent : BaseEntity, INotification
{
    public string CouponCode { get; set; }

    public decimal DiscountAmount { get; set; }

    public DateTime CreatedDate { get; set; }

    public int MinAmount { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int UsageLimit { get; set; }
    public decimal DiscountPercentage { get; set; }
    public CouponType Type { get; set; }

    public CreateCouponEvent(Guid id,string couponCode, decimal discountAmount,
        DateTime createdDate,int minAmount, DateTime expirationDate, int usageLimit,
         decimal discountPercentage,CouponType type)
    {
        Id = id;
        CouponCode = couponCode;
        DiscountAmount = discountAmount;
        CreatedDate = createdDate;
        MinAmount = minAmount;
        ExpirationDate = expirationDate;
        UsageLimit = usageLimit;
        DiscountPercentage = discountPercentage;
        Type = type;
    }
}