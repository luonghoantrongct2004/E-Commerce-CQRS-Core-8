using E.Domain.Entities.Products;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace E.Domain.Entities.Orders;

public class OrderDetail : BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }

    [BsonRepresentation(BsonType.String)]
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime? ShipDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public OrderDetail()
    {
        CreatedAt = DateTime.UtcNow;
    }
}