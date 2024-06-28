using System.ComponentModel.DataAnnotations;

namespace E.API.Contracts.Products.Requests;

public class ProductUpdate
{
    public string ProductName { get; set; }

    public string? Description { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int Price { get; set; }

    public List<string>? Images { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? BrandId { get; set; }

    public int StockQuantity { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int? Discount { get; set; }
}
