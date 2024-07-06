using E.Application.Services.UserServices.UserValidators;
using E.Domain.Entities.Users;
using E.Domain.Exceptions;

namespace E.Application.Services.UserServices;

public class UserValidationService
{
    private readonly UserValidator _validator = new UserValidator();

    public void ValidateAndThrow(DomainUser user)
    {
        var validationResult = _validator.Validate(user);
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