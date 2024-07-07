namespace E.Application.Coupons;

public class CouponErrorMessage
{
    public const string CouponNotFound = "Coupon not found.";
    public static string CouponExpirationDate(string couponName) => $"Coupon {couponName} is expired.";
    public static string CouponNotActive(string couponName) => $"Coupon {couponName} is not active.";
    public const string CouponUsageLimitReached = "Coupon usage limit reached.";
    public static string TotalPriceLessThanMinimum(int minimumAmount) => 
        $"Total price is less than the minimum amount {minimumAmount}.";
    public const string CouponAlreadyApplied = "Coupon has already been applied.";
}