using E.Application.Services.CategoryServices.CategoryValidators;
using E.Domain.Entities.Categories;
using E.Domain.Exceptions;

namespace E.Application.Services.CategoryServices;

public class CategoryValidationService
{
    private readonly CategoryValidator _validator = new CategoryValidator();

    public void ValidateAndThrow(Category category)
    {
        var validationResult = _validator.Validate(category);
        if (!validationResult.IsValid)
        {
            var exception = new CategoryInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
}