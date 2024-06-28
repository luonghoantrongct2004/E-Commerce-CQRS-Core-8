using E.Domain.Entities.Brand.BrandValidators;
using E.Domain.Entities.Products;
using E.Domain.Exceptions;

namespace E.Domain.Entities.Brand;

public class Brand : BaseEntity
{
    public string BrandName { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();

    public static Brand CreateBrand(string brandName)
    {
        var validator = new BrandValidator();
        var objectToValidate = new Brand
        {
            Id = Guid.NewGuid(),
            BrandName = brandName,
        };
        var validationResult = validator.Validate(objectToValidate);
        if (validationResult.IsValid) return objectToValidate;
        var exception = new BrandInvalidException($"{validationResult}");
        foreach (var error in validationResult.Errors)
        {
            exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
        }
        throw exception;
    }

    public void UpdateBrand(string brandName)
    {
        BrandName = brandName;

        var validator = new BrandValidator();
        var validationResult = validator.Validate(this);
        if (!validationResult.IsValid)
        {
            var exception = new ProductInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
}