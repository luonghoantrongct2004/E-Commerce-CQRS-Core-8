using E.Domain.Entities.Brand;
using E.Domain.Entities.Carts;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Comment;
using E.Domain.Entities.Coupons;
using E.Domain.Entities.Introductions;
using E.Domain.Entities.News;
using E.Domain.Entities.Orders;
using E.Domain.Entities.Products;
using E.Domain.Entities.Users;
using E.Infrastructure.Repository.Interfaces;
using E.Infrastructure.Repository.MongoRepositories;
using MongoDB.Driver;

namespace E.DAL.UoW;

public class ReadUnitOfWork : IReadUnitOfWork
{
    public IReadRepository<Product> Products { get; }
    public IReadRepository<Category> Categories { get; }
    public IReadRepository<Brand> Brands { get; }
    public IReadRepository<UserMongo> Users { get; }
    public IReadRepository<Comment> Comments { get; }
    public IReadRepository<Coupon> Coupons { get; }
    public IReadRepository<Introduction> Introductions { get; }
    public IReadRepository<New> News { get; }
    public IReadRepository<Order> Orders { get; }
    public IReadRepository<CartDetails> Carts { get; }

    public ReadUnitOfWork(IMongoDatabase database)
    {
        Products = new MongoRepository<Product>(database, "Products");
        Categories = new MongoRepository<Category>(database, "Categories");
        Brands = new MongoRepository<Brand>(database, "Brands");
        Users = new MongoRepository<UserMongo>(database, "Users");
        Comments = new MongoRepository<Comment>(database, "Comments");
        Coupons = new MongoRepository<Coupon>(database, "Coupons");
        Introductions = new MongoRepository<Introduction>(database, "Introductions");
        News = new MongoRepository<New>(database, "News");
        Orders = new MongoRepository<Order>(database, "Orders");
        Carts = new MongoRepository<CartDetails>(database, "Carts");
    }
}
