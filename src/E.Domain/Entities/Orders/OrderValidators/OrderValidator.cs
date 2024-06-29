using FluentValidation;

namespace E.Domain.Entities.Orders.OrderValidations;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(o => o.UserId)
                .NotEmpty().WithMessage("User ID must be specified.");

        RuleFor(o => o.OrderDate)
            .NotNull().WithMessage("Order date must be specified.");

        RuleFor(o => o.PaymentDate)
            .NotNull().WithMessage("Payment date must be specified.");

        RuleFor(o => o.PaymentId)
            .NotEmpty().WithMessage("Payment ID must be specified.");

        RuleFor(o => o.PaymentMethod)
            .NotEmpty().WithMessage("Payment method must be specified.");

        RuleFor(o => o.ShippingAddress)
            .NotEmpty().WithMessage("Shipping address must be specified.");

        RuleFor(o => o.ContactPhone)
            .NotEmpty().WithMessage("Contact phone must be specified.");

        RuleFor(o => o.TotalPrice)
            .GreaterThan(0).WithMessage("Total price must be greater than 0.");

    }
}