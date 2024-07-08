using E.Domain.Enum;

namespace E.API.Contracts.Coupons.Requests;

public class CreateCoupon
{
    public string CouponCode { get; set; }

    public decimal DiscountAmount { get; set; }

    public int MinAmount { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int UsageLimit { get; set; }
    public decimal DiscountPercentage { get; set; }
    public CouponType Type { get; set; }
}
