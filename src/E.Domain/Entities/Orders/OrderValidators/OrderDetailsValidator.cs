using FluentValidation;

namespace E.Domain.Entities.Orders.OrderValidators;

public class OrderDetailsValidator : AbstractValidator<Orderdetail>
{
    public OrderDetailsValidator()
    {
        RuleFor(od => od.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        RuleFor(od => od.ShipDate)
            .NotNull().WithMessage("Ship date must be specified.");

        RuleFor(od => od.ProductId)
            .NotEmpty().WithMessage("Product ID must be specified.");
    }
}