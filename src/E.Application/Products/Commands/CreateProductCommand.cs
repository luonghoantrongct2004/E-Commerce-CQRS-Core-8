using E.Application.Models;
using E.Domain.Entities.Products;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E.Application.Products.Commands;

public class CreateProductCommand : IRequest<OperationResult<Product>>
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

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int Discount { get; set; }
}