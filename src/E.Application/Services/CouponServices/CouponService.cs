using E.Domain.Entities.Coupons;

namespace E.Application.Services.CouponServices;

public class CouponService
{
    private readonly CouponValidationService _validationService;

    public CouponService(CouponValidationService validationService)
    {
        _validationService = validationService;
    }
    public Coupon CreateCoupon(string couponCode)
    {
        var objectToValidate = new Coupon
        {
            CouponCode = couponCode,
        };
        _validationService.ValidateAndThrow(objectToValidate);
        return objectToValidate;
    }

    public void UpdateCoupon(Coupon Coupon, string couponCode)
    {
        Coupon.CouponCode = couponCode;
        _validationService.ValidateAndThrow(Coupon);
    }

    public void DisableCoupon(Coupon Coupon)
    {
        Coupon.IsActive = false;
        _validationService.ValidateAndThrow(Coupon);
    }
}