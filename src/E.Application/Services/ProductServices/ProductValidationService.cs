using E.Application.Services.ProductServices.ProductValidators;
using E.Domain.Entities.Products;
using E.Domain.Exceptions;

namespace E.Application.Services.ProductServices;

public class ProductValidationService
{
    private readonly ProductValidator _validator = new ProductValidator();

    public void ValidateAndThrow(Product product)
    {
        var validationResult = _validator.Validate(product);
        if (!validationResult.IsValid)
        {
            var exception = new BrandInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
}