using E.Domain.Entities.Categories;
using E.Domain.Entities.Orders;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

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

    public bool IsActive { get; set; } = true;

    public Category? Category { get; set; }
    public Brand.Brand? Brand { get; set; }
    public IEnumerable<Comment.Comment>? Comments { get; set; }
    public virtual IEnumerable<OrderDetail>? OrderDetails { get; set; }
}