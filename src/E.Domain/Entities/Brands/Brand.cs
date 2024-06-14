using E.Domain.Entities.Brand.BrandValidators;
using E.Domain.Entities.Products;
using E.Domain.Exceptions;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace E.Domain.Entities.Brand;

public class Brand
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid BrandId { get; set; }

    public string BrandName { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public static Brand CreateBrand(string brandName)
    {
        var validator = new BrandValidator();
        var objectToValidate = new Brand
        {
            BrandId = Guid.NewGuid(),
            BrandName = brandName,
        };
        var validationResult = validator.Validate(objectToValidate);
        if (validationResult.IsValid) return objectToValidate;
        var exception = new BrandInvailidException($"{validationResult}");
        foreach (var error in validationResult.Errors)
        {
            exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
        }
        throw exception;
    }
}
