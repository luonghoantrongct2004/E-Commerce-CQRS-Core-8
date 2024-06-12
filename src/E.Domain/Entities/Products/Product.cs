using System.ComponentModel.DataAnnotations;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Products.ProductValidators;
using E.Domain.Exceptions;
using E.Domain.Models;
using FluentValidation.Results;

namespace E.Domain.Entities.Products;

public class Product
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string? Description { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int Price { get; set; }

    public List<string>? Images { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? BrandId { get; set; }
    public Guid? CommentId { get; set; }

    public int StockQuantity { get; set; }
    public int? SoldQuantity { get; set; } = 0;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int? Discount { get; set; }

    public Category? Category { get; set; }
    public E.Domain.Entities.Brand.Brand? Brand { get; set; }
    public IEnumerable<E.Domain.Entities.Comment.Comment>? Comments { get; set; }
    public ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public static Product CreateProduct(string productName, string description, int price, List<string> images,
        Guid categoryId, Guid brandId, int stockQuantity, int discount)
    {
        var validator = new ProductValidator();
        var objectToValidate = new Product
        {
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
        var validationResult = validator.Validate(objectToValidate);
        if (validationResult.IsValid) return objectToValidate;
        var exception = new ProductInvailidException($"{validationResult}");
        foreach (var error in validationResult.Errors)
        {
            exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
        }
        throw exception;
    }
}
