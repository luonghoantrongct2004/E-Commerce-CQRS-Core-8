using E.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace E.API.Contracts.Products.Responses;

public class ProductResponse
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string? Description { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int Price { get; set; }

    public List<string>? Images { get; set; }

    public int? CategoryId { get; set; }

    public int? BrandId { get; set; }
    public int? CommentId { get; set; }

    public int StockQuantity { get; set; }
    public int? SoldQuantity { get; set; } = 0;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int? Discount { get; set; }

    public Category? Category { get; set; }
    public E.Domain.Entities.Brand.Brand? Brand { get; set; }
    public IEnumerable<E.Domain.Entities.Comment.Comment>? Comments { get; set; }
}
