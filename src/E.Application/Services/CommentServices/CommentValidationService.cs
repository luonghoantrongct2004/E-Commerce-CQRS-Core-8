using E.Application.Services.CommentServices.CommentValidators;
using E.Domain.Entities.Comment;
using E.Domain.Exceptions;

namespace E.Application.Services.CommentServices;

public class CommentValidationService
{
    private readonly CommentValidator _validator = new CommentValidator();

    public void ValidateAndThrow(Comment comment)
    {
        var validationResult = _validator.Validate(comment);
        if (!validationResult.IsValid)
        {
            var exception = new CommentInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
}