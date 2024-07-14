using E.Domain.Entities.Brand;
using E.Domain.Entities.Carts;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Comment;
using E.Domain.Entities.Coupons;
using E.Domain.Entities.Introductions;
using E.Domain.Entities.News;
using E.Domain.Entities.Orders;
using E.Domain.Entities.Products;
using E.Domain.Entities.Token;
using E.Domain.Entities.Users;
using E.Infrastructure.Repository.Interfaces;

namespace E.DAL.UoW;

public interface IReadUnitOfWork
{
    IReadRepository<Product> Products { get; }
    IReadRepository<Category> Categories { get; }
    IReadRepository<Brand> Brands { get; }
    IReadRepository<UserMongo> Users { get; }
    IReadRepository<CartDetails> Carts { get; }
    IReadRepository<Comment> Comments { get; }
    IReadRepository<Coupon> Coupons { get; }
    IReadRepository<Introduce> Introductions { get; }
    IReadRepository<New> News{ get; }
    IReadRepository<Order> Orders{ get; }
    IReadRepository<RefreshToken> Tokens { get; }
}
