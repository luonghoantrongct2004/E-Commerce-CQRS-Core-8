using E.Application.Models;
using E.Domain.Entities.Products;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E.Application.Products.Commands;

public class UpdateProductCommand : IRequest<OperationResult<Product>>
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string? Description { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public decimal Price { get; set; }

    public List<string>? Images { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? BrandId { get; set; }
    public Guid? CommentId { get; set; }

    public int StockQuantity { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int? Discount { get; set; }
}