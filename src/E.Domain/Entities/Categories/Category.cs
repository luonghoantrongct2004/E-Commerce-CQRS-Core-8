using E.Domain.Entities.Categories.CategoryValidators;
using E.Domain.Entities.Products;
using E.Domain.Exceptions;

namespace E.Domain.Entities.Categories;

public class Category : BaseEntity
{
    public string? CategoryName { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
    private readonly CategoryValidator _validator;

    public Category()
    {
        _validator = new CategoryValidator();
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

    public static Category CreateCategory(string categoryName)
    {
        var objectToValidate = new Category
        {
            Id = Guid.NewGuid(),
            CategoryName = categoryName,
        };
        objectToValidate.ValidateAndThrow();

        return objectToValidate;
    }

    public void UpdateCategory(string categoryName)
    {
        CategoryName = categoryName;
        ValidateAndThrow();
    }

    public void DeleteCategory(Guid categoryId)
    {
        Id = categoryId;
        ValidateAndThrow();
    }
}