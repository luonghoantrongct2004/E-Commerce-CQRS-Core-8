using E.Domain.Entities.Introductions;
using FluentValidation;

namespace E.Application.Services.IntroduceServices.IntroductionValidators;

public class IntroduceValidator : AbstractValidator<Introduce>
{
    public IntroduceValidator()
    {
        RuleFor(intro => intro.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(intro => intro.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(intro => intro.ImageUrl)
            .Must(urls => urls == null || urls.Count > 0).WithMessage("At least one image URL is required.");

        RuleFor(intro => intro.CreatedAt)
            .NotEmpty().WithMessage("Created date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Created date must be in the past.");
    }
}