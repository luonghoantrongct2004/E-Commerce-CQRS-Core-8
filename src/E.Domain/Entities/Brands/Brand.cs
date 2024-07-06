using E.Domain.Entities.Products;

namespace E.Domain.Entities.Brand;

public class Brand : BaseEntity
{
    public string BrandName { get; set; }
    public bool IsDelete { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}