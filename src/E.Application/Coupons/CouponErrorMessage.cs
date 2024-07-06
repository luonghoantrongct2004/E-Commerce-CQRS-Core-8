namespace E.Application.Coupons;

public class CouponErrorMessage
{
    public const string CouponNotFound = "Coupon not found.";
    public const string CouponExpirationDate = "Coupon {0} is expired.";
    public const string CouponNotActive = "Coupon {0} is not active.";
    public const string CouponUsageLimitReached = "Coupon usage limit reached.";
    public const string TotalPriceLessThanMinimum = "Total price is less than the minimum amount {0}.";
    public const string CouponAlreadyApplied = "Coupon has already been applied.";
}