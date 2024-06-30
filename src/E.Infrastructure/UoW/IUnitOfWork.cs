using E.DAL.Repository;
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

namespace E.DAL.UoW;

public interface IUnitOfWork
{
    IRepository<Product> Products { get; }
    IRepository<Category> Categories { get; }
    IRepository<Brand> Brands { get; }
    IRepository<DomainUser> Users { get; }
    IRepository<CartDetails> Carts { get; }
    IRepository<Comment> Comments { get; }
    IRepository<Coupon> Coupons { get; }
    IRepository<Introduction> Introductions { get; }
    IRepository<New> News { get; }
    IRepository<Order> Orders { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task<int> CompleteAsync();
    void Dispose();
}
