using E.Domain.Entities.Comment;
using FluentValidation;

namespace E.Application.Services.CommentServices.CommentValidators;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Comment content can't be empty!");
    }
}