using System.ComponentModel.DataAnnotations;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Products.ProductValidators;
using E.Domain.Exceptions;
using E.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using E.Domain.Entities.Brand.BrandValidators;

namespace E.Domain.Entities.Products;

public class Product : BaseEntity
{
    public string ProductName { get; set; }

    public string? Description { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int Price { get; set; }

    public List<string>? Images { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid CategoryId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid BrandId { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid? CommentId { get; set; }

    public int StockQuantity { get; set; }
    public int? SoldQuantity { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int Discount { get; set; }

    public Category? Category { get; set; }
    public E.Domain.Entities.Brand.Brand? Brand { get; set; }
    public IEnumerable<E.Domain.Entities.Comment.Comment>? Comments { get; set; }
    public ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    private readonly ProductValidator _validator;
    public Product()
    {
        _validator = new ProductValidator();
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
    public static Product CreateProduct(string productName, string description, int price, List<string> images,
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
        objectToValidate.ValidateAndThrow();

        return objectToValidate;
    }
    public void UpdateProduct(string productName, string? description, int price, List<string>? images,
           Guid categoryId, Guid brandId, int stockQuantity, int discount)
    {
        ProductName = productName;
        Description = description;
        Price = price;
        Images = images;
        CategoryId = categoryId;
        BrandId = brandId;
        StockQuantity = stockQuantity;
        Discount = discount;

        ValidateAndThrow();
    }
    public void DeleteProduct(Guid productId)
    {
        Id = productId;
        ValidateAndThrow();
    }
}
