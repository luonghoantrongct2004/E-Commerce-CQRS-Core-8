using FluentValidation;

namespace E.Domain.Entities.Brand.BrandValidators;

public class BrandValidator:AbstractValidator<Brand>
{
    public BrandValidator()
    {
        RuleFor(x => x.BrandName)
            .NotEmpty().WithMessage("Brand Name can't be empty!");
    }
}