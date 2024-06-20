using E.DAL.Repository;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Products;
using E.Domain.Entities.Users;
using MongoDB.Driver;

namespace E.DAL.UoW;

public class ReadUnitOfWork : IReadUnitOfWork
{
    public IReadRepository<Product> Products { get; }
    public IReadRepository<Category> Categories { get; }
    public IReadRepository<Brand> Brands { get; }

    public IReadRepository<User> Users { get; }

    public ReadUnitOfWork(IMongoDatabase database)
    {
        Products = new MongoRepository<Product>(database, "Products");
        Categories = new MongoRepository<Category>(database, "Categories");
        Brands = new MongoRepository<Brand>(database, "Brands");
    }
}