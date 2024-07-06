using E.Application.Services.NewServices.NewValidators;
using E.Domain.Entities.News;
using E.Domain.Exceptions;

namespace E.Application.Services.NewServices;

public class NewValidationService
{
    private readonly NewValidator _validator = new NewValidator();

    public void ValidateAndThrow(New news)
    {
        var validationResult = _validator.Validate(news);
        if (!validationResult.IsValid)
        {
            var exception = new NewInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
}