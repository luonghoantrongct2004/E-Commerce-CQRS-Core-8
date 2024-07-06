using E.Domain.Entities.Orders;
using FluentValidation;

namespace E.Application.Services.OrderServices.OrderValidators;

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

        RuleFor(o => o.PaymentMethod)
            .NotEmpty().WithMessage("Payment method must be specified.");

        RuleFor(o => o.ContactPhone)
            .NotEmpty().WithMessage("Contact phone must be specified.");

        RuleFor(o => o.TotalPrice)
            .GreaterThan(0).WithMessage("Total price must be greater than 0.");

    }
}