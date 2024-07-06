using E.Domain.Entities.Products;

namespace E.Domain.Entities.Brand;

public class Brand : ActiveEntity
{
    public string BrandName { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}