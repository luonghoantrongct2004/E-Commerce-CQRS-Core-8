using E.Domain.Entities.Products;
using FluentValidation;

namespace E.Application.Services.ProductServices.ProductValidators;

public class ProductValidator:AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.ProductName)
            .NotEmpty().WithMessage("Product can't be empty!");
        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("Price can't be empty!")
            .GreaterThanOrEqualTo(0).WithMessage("Price can't be less than 0!");

        RuleFor(p => p.StockQuantity)
            .NotEmpty().WithMessage("Stock quantity can't be empty!")
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity can't be less than 0!");

        RuleFor(p => p.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity can't less than 0!");
    }
}