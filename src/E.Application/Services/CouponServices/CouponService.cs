using E.Application.Coupons;
using E.Domain.Entities.Carts;
using E.Domain.Entities.Coupons;
using E.Domain.Enum;

namespace E.Application.Services.CouponServices;

public class CouponService
{
    private readonly CouponValidationService _validationService;

    public CouponService(CouponValidationService validationService)
    {
        _validationService = validationService;
    }

    public void ApplyCoupon(Coupon coupon)
    {
        coupon.UsageLimit -= 1;
        _validationService.ValidateAndThrow(coupon);
    }

    public Coupon CreateCoupon(string couponCode, decimal discountAmount,
        int minAmount, DateTime expirationDate, int usageLimit, decimal discountPercentage,
        CouponType type)
    {
        var objectToValidate = new Coupon
        {
            CouponCode = couponCode,
            DiscountAmount = discountAmount,
            MinAmount = minAmount,
            ExpirationDate = expirationDate,
            UsageLimit = usageLimit,
            DiscountPercentage = discountPercentage,
            Type = type
        };
        _validationService.ValidateAndThrow(objectToValidate);
        return objectToValidate;
    }

    public void UpdateCoupon(Coupon coupon, string couponCode,
        decimal discountAmount,int minAmount,
        DateTime expirationDate, int usageLimit)
    {
        coupon.CouponCode = couponCode;
        coupon.DiscountAmount = discountAmount;
        coupon.MinAmount = minAmount;
        coupon.CreatedDate = DateTime.Now;
        coupon.ExpirationDate = expirationDate;
        coupon.UsageLimit = usageLimit;
        _validationService.ValidateAndThrow(coupon);
    }

    public void DisableCoupon(Coupon Coupon)
    {
        Coupon.IsActive = false;
        _validationService.ValidateAndThrow(Coupon);
    }
}