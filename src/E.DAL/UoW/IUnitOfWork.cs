using E.DAL.Repository;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Products;
using E.Domain.Models;

namespace E.DAL.UoW;

public interface IUnitOfWork
{
    IRepository<Product> Products { get; }
    IRepository<Category> Categories { get; }
    IRepository<Brand> Brands { get; }
    Task<int> CompleteAsync();
}
