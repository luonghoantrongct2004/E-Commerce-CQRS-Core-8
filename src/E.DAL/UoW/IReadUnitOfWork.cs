using E.DAL.Repository;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Products;
using MongoDB.Driver;

namespace E.DAL.UoW;

public interface IReadUnitOfWork
{
    IReadRepository<Product> Products { get; }
    IReadRepository<Category> Categories { get; }
    IReadRepository<Brand> Brands { get; }
}
