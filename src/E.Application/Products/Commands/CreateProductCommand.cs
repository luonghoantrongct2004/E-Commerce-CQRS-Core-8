using E.Application.Models;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Products;
using E.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E.Application.Products.Commands;

public class CreateProductCommand : IRequest<OperationResult<Product>>
{
    public string ProductName { get; set; }

    public string? Description { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int   Price { get; set; }

    public List<string>? Images { get; set; }

    public Guid CategoryId { get; set; }

    public Guid BrandId { get; set; }
    public Guid? CommentId { get; set; }

    public int StockQuantity { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int Discount { get; set; }
}