using FluentValidation;

namespace E.Domain.Entities.Categories.CategoryValidators;

public class CategoryValidator:AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.CategoryName)
            .NotEmpty().WithMessage("Category can't be empty!");
    }
}