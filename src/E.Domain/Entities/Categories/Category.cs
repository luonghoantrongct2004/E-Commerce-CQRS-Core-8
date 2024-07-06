using E.Domain.Entities.Products;

namespace E.Domain.Entities.Categories;

public class Category : ActiveEntity
{
    public string? CategoryName { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}