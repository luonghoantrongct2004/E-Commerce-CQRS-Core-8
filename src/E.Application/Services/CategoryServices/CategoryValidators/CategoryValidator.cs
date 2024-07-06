using E.Domain.Entities.Categories;
using FluentValidation;

namespace E.Application.Services.CategoryServices.CategoryValidators;

public class CategoryValidator:AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.CategoryName)
            .NotEmpty().WithMessage("Category can't be empty!");
    }
}