using FluentValidation;

namespace E.Domain.Entities.Coupons.CouponValidators;

public class CouponValidator : AbstractValidator<Coupon>
{
    public CouponValidator()
    {
        RuleFor(c => c.CouponCode)
            .NotEmpty().WithMessage("Coupon code is required.")
            .Length(5, 20).WithMessage("Coupon code must be between 5 and 20 characters.");

        RuleFor(c => c.DiscountAmount)
            .GreaterThan(0).WithMessage("Discount amount must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Discount amount can't exceed 100.");

        RuleFor(c => c.MinAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum amount can't be less than 0.");

        RuleFor(c => c.ExpirationDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");

        RuleFor(c => c.UsageLimit)
            .GreaterThanOrEqualTo(0).WithMessage("Usage limit can't be less than 0.");

        RuleFor(c => c.IsActive)
            .Must(isActive => isActive == true || isActive == false)
            .WithMessage("Active status must be either true or false.");
    }
}
