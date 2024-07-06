using E.Domain.Entities.Carts;
using FluentValidation;

namespace E.Application.Services.CartServices.CartValidators;

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

        RuleFor(cd => cd.CartTotal)
            .GreaterThanOrEqualTo(0).WithMessage("CartTotal can't be less than 0.");
    }
}