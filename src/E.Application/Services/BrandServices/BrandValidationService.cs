using E.Domain.Entities.Brand;
using E.Domain.Exceptions;

namespace E.Application.Services.BrandServices;

public class BrandValidationService
{
    private readonly BrandValidator _validator = new BrandValidator();

    public void ValidateAndThrow(Brand brand)
    {
        var validationResult = _validator.Validate(brand);
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