using E.Domain.Entities.Categories;

namespace E.Application.Services.CategoryServices;

public class CategoryServices
{
    private readonly CategoryValidationService _validationService;

    public CategoryServices(CategoryValidationService validationService)
    {
        _validationService = validationService;
    }

    public Category CreateCategory(string categoryName)
    {
        var objectToValidate = new Category
        {
            CategoryName = categoryName,
        };
        _validationService.ValidateAndThrow(objectToValidate);
        return objectToValidate;
    }

    public void UpdateCategory(Category category, string categoryName)
    {
        category.CategoryName = categoryName;
        _validationService.ValidateAndThrow(category);
    }

    public void DisableCategory(Category category)
    {
        category.IsActive = false;
        _validationService.ValidateAndThrow(category);
    }
}