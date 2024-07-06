using E.Domain.Entities.Products;

namespace E.Application.Services.ProductServices;

public class ProductService
{
    private readonly ProductValidationService _validationService;

    public ProductService(ProductValidationService validationService)
    {
        _validationService = validationService;
    }

    public Product CreateProduct(string productName, string description, int price, List<string> images,
        Guid categoryId, Guid brandId, int stockQuantity, int discount)
    {
        var objectToValidate = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = productName,
            Description = description,
            Price = price,
            Images = images,
            CategoryId = categoryId,
            BrandId = brandId,
            StockQuantity = stockQuantity,
            Discount = discount,
            CreatedAt = DateTime.UtcNow
        };
        _validationService.ValidateAndThrow(objectToValidate);

        return objectToValidate;
    }

    public void UpdateProduct(Product product,
        string productName, string? description, int price, List<string>? images,
           Guid categoryId, Guid brandId, int stockQuantity, int discount)
    {
        product.ProductName = productName;
        product.Description = description;
        product.Price = price;
        product.Images = images;
        product.CategoryId = categoryId;
        product.BrandId = brandId;
        product.StockQuantity = stockQuantity;
        product.Discount = discount;

        _validationService.ValidateAndThrow(product);
    }

    public void DisableProduct(Product product)
    {
        product.IsActive = false;
        _validationService.ValidateAndThrow(product);
    }
}