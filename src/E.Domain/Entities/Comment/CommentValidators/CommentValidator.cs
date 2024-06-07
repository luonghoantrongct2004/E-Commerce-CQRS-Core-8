using FluentValidation;

namespace E.Domain.Entities.Comment.CommentValidators;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Comment content can't be empty!");
    }
}