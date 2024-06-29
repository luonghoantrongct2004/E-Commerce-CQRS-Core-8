using FluentValidation;

namespace E.Domain.Entities.Carts.CartValidators;

public class CartValidator : AbstractValidator<CartDetails>
{
    public CartValidator()
    {
        RuleFor(cd => cd.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity can't be less than 0!");

        RuleFor(cd => cd.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(cd => cd.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(cd => cd.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount can't be less than 0.")
            .LessThanOrEqualTo(100).WithMessage("Discount can't be more than 100.");

        RuleFor(cd => cd.CartTotal)
            .GreaterThanOrEqualTo(0).WithMessage("CartTotal can't be less than 0.");
    }
}