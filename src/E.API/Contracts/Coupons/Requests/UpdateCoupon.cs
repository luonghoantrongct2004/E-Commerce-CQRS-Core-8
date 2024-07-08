namespace E.API.Contracts.Coupons.Requests;

public class UpdateCoupon
{
    public Guid Id { get; set; }
    public string CouponCode { get; set; }

    public decimal DiscountAmount { get; set; }

    public int MinAmount { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int UsageLimit { get; set; }
}
