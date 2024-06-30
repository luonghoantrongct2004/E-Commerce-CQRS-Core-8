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
using Microsoft.EntityFrameworkCore.Storage;
using System.Xml.Linq;

namespace E.DAL.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<Product>? _productRepository;
    private IRepository<Category>? _categoryRepository;
    private IRepository<Brand>? _brandRepository;
    private IRepository<DomainUser>? _userRepository;
    private IRepository<CartDetails>? _cartRepository;
    private IRepository<Comment>? _commentRepository;
    private IRepository<Coupon>? _couponRepository;
    private IRepository<Introduction>? _introductionRepository;
    private IRepository<New>? _newRepository;
    private IRepository<Order>? _orderRepository;
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IRepository<Product> Products => _productRepository ??= new SqlRepository<Product>(_context);
    public IRepository<Category> Categories => _categoryRepository ??= new SqlRepository<Category>(_context);
    public IRepository<Brand> Brands => _brandRepository ??= new SqlRepository<Brand>(_context);
    public IRepository<DomainUser> Users => _userRepository ??= new SqlRepository<DomainUser>(_context);
    public IRepository<CartDetails> Carts => _cartRepository ??= new SqlRepository<CartDetails>(_context);
    public IRepository<Comment> Comments => _commentRepository ??= new SqlRepository<Comment>(_context);
    public IRepository<Coupon> Coupons => _couponRepository ??= new SqlRepository<Coupon>(_context);
    public IRepository<Introduction> Introductions => _introductionRepository ??= new SqlRepository<Introduction>(_context);
    public IRepository<New> News => _newRepository ??= new SqlRepository<New>(_context);
    public IRepository<Order> Orders => _orderRepository ??= new SqlRepository<Order>(_context);

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            _transaction.Dispose();
        }
    }

    public async Task RollbackAsync()
    {
        try
        {
            await _transaction.RollbackAsync();
        }
        finally
        {
            _transaction.Dispose();
        }
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}