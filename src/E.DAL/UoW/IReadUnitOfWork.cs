using E.DAL.Repository;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Products;
using E.Domain.Entities.Users;

namespace E.DAL.UoW;

public interface IReadUnitOfWork
{
    IReadRepository<Product> Products { get; }
    IReadRepository<Category> Categories { get; }
    IReadRepository<Brand> Brands { get; }
    IReadRepository<UserMongo> Users { get; }
    object Category { get; }
}
