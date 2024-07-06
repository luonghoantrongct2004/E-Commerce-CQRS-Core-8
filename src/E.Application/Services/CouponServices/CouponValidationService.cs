using E.Application.Services.CouponServices.CouponValidators;
using E.Domain.Entities.Coupons;
using E.Domain.Exceptions;

namespace E.Application.Services.CouponServices;

public class CouponValidationService
{
    private readonly CouponValidator _validator = new CouponValidator();

    public void ValidateAndThrow(Coupon coupon)
    {
        var validationResult = _validator.Validate(coupon);
        if (!validationResult.IsValid)
        {
            var exception = new CouponInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
}