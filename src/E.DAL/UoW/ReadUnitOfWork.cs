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

    public IReadRepository<UserMongo> Users { get; }

    IReadRepository<Product> IReadUnitOfWork.Products => throw new NotImplementedException();

    IReadRepository<Category> IReadUnitOfWork.Categories => throw new NotImplementedException();

    IReadRepository<Brand> IReadUnitOfWork.Brands => throw new NotImplementedException();

    IReadRepository<UserMongo> IReadUnitOfWork.Users => throw new NotImplementedException();

    object IReadUnitOfWork.Category => throw new NotImplementedException();

    public ReadUnitOfWork(IMongoDatabase database)
    {
        Products = new MongoRepository<Product>(database, "Products");
        Categories = new MongoRepository<Category>(database, "Categories");
        Brands = new MongoRepository<Brand>(database, "Brands");
        Users = new MongoRepository<UserMongo>(database, "Users");
    }
}