using E.Domain.Entities.Orders;
using FluentValidation;

namespace E.Application.Services.OrderServices.OrderValidators;

public class OrderDetailsValidator : AbstractValidator<OrderDetail>
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