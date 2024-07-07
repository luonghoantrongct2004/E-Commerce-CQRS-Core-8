using E.Application.Services.OrderServices.OrderValidators;
using E.Domain.Entities.Orders;
using E.Domain.Exceptions;

namespace E.Application.Services.OrderServices;

public class OrderValidationService
{
    private readonly OrderValidator _validator = new OrderValidator();

    public void ValidateAndThrow(Order order)
    {
        var validationResult = _validator.Validate(order);
        if (!validationResult.IsValid)
        {
            var exception = new OrderInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
}