using E.Application.Services.IntroduceServices.IntroductionValidators;
using E.Domain.Entities.Introductions;
using E.Domain.Exceptions;

namespace E.Application.Services.IntroduceServices;

public class IntroduceValidationService
{
    private readonly IntroduceValidator _validator = new IntroduceValidator();

    public void ValidateAndThrow(Introduce introduce)
    {
        var validationResult = _validator.Validate(introduce);
        if (!validationResult.IsValid)
        {
            var exception = new IntroductionInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
}