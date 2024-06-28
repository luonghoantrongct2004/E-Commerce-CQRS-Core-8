using E.Domain.Entities.Brand.BrandValidators;
using E.Domain.Entities.Products;
using E.Domain.Exceptions;

namespace E.Domain.Entities.Brand;

public class Brand : BaseEntity
{
    public string BrandName { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();

    private readonly BrandValidator _validator;
    public Brand()
    {
        _validator = new BrandValidator();
    }
    private void ValidateAndThrow()
    {
        var validationResult = _validator.Validate(this);
        if (!validationResult.IsValid)
        {
            var exception = new BrandInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
    public static Brand CreateBrand(string brandName)
    {
        var objectToValidate = new Brand
        {
            Id = Guid.NewGuid(),
            BrandName = brandName,
        };
        objectToValidate.ValidateAndThrow();

        return objectToValidate;
    }

    public void UpdateBrand(string brandName)
    {
        BrandName = brandName;
        ValidateAndThrow();
    }
    public void DeleteBrand(Guid brandId)
    {
        Id = brandId;
        ValidateAndThrow();
    }
}