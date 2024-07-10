using E.Application.Services.OrderServices.OrderValidators;
using E.Domain.Entities.Orders;
using E.Domain.Exceptions;
using FluentValidation;

namespace E.Application.Services.OrderServices;

public class OrderDetailValidationService
{
    private readonly OrderDetailsValidator _validator = new OrderDetailsValidator();
    public void ValidateAndThrow(OrderDetail orderdetail)
    {
        var validationResult = _validator.Validate(orderdetail);
        if (!validationResult.IsValid)
        {
            var exception = new OrderDetailsInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }

}