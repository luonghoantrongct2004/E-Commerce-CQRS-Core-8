using E.Domain;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Comment;
using E.Domain.Entities.Products;
using E.Domain.Entities.Users;
using E.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace E.DAL;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
    {
        _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
    }
    public IMongoDatabase Database => _database;

    public IMongoCollection<UserMongo> Users => _database.GetCollection<UserMongo>("Users");
    public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    public IMongoCollection<Orderdetail> Orderdetails => _database.GetCollection<Orderdetail>("Orderdetails");
    public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
    public IMongoCollection<Brand> Brands => _database.GetCollection<Brand>("Brands");
    public IMongoCollection<CartDetails> CartDetails => _database.GetCollection<CartDetails>("CartDetails");
    public IMongoCollection<Coupon> Coupons => _database.GetCollection<Coupon>("Coupons");
    public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
    public IMongoCollection<Introduction> Introductions => _database.GetCollection<Introduction>("Introductions");
    public IMongoCollection<News> News => _database.GetCollection<News>("News");

}