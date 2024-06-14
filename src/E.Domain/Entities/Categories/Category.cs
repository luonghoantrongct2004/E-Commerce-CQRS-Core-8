using E.Domain.Entities.Categories.CategoryValidators;
using E.Domain.Entities.Products;
using E.Domain.Exceptions;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace E.Domain.Entities.Categories;

public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid CategoryId { get; set; }

    public string? CategoryName { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();

    public static Category CreateCategory(string categoryName)
    {
        var validator = new CategoryValidator();
        var objectToValidate = new Category
        {
            CategoryId = Guid.NewGuid(),
            CategoryName = categoryName,
        };
        var validationResult = validator.Validate(objectToValidate);
        if (validationResult.IsValid) return objectToValidate;
        var exception = new CategoryInvailidException($"{validationResult}");
        foreach (var error in validationResult.Errors)
        {
            exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
        }
        throw exception;
    }
}
