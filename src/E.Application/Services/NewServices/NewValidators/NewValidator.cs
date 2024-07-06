using E.Domain.Entities.News;
using FluentValidation;

namespace E.Application.Services.NewServices.NewValidators;

public class NewValidator : AbstractValidator<New>
{
    public NewValidator()
    {
        RuleFor(news => news.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(news => news.Content)
            .NotEmpty().WithMessage("Content is required.");

        RuleFor(news => news.Author)
            .NotEmpty().WithMessage("Author name is required.");

        RuleFor(news => news.Image)
            .Must(images => images == null || images.Count > 0).WithMessage("At least one image URL is required.");

        RuleFor(news => news.PublishedAt)
            .NotEmpty().WithMessage("Published date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Published date must be in the past.");
    }
}