using E.Application.Services.CartServices.CartValidators;
using E.Domain.Entities.Carts;
using E.Domain.Exceptions;

namespace E.Application.Services.CartServices;

public class CartValidationService
{
    private readonly CartValidator _validator = new CartValidator();

    public void ValidateAndThrow(CartDetails cart)
    {
        var validationResult = _validator.Validate(cart);
        if (!validationResult.IsValid)
        {
            var exception = new CartInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
}