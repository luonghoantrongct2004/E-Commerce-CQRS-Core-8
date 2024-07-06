using E.Domain.Entities.Brand;
using FluentValidation;

namespace E.Application.Services.BrandServices;

public class BrandValidator:AbstractValidator<Brand>
{
    public BrandValidator()
    {
        RuleFor(x => x.BrandName)
            .NotEmpty().WithMessage("Brand Name can't be empty!");
    }
}