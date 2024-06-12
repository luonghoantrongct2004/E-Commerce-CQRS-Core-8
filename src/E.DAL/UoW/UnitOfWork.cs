using E.DAL.Repository;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore.Storage;

namespace E.DAL.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<Product>? _productRepository;
    private IRepository<Category>? _categoryRepository;
    private IRepository<Brand>? _brandRepository;
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IRepository<Product> Products => _productRepository ??= new SqlRepository<Product>(_context);
    public IRepository<Category> Categories => _categoryRepository ??= new SqlRepository<Category>(_context);

    public IRepository<Brand> Brands => _brandRepository ??= new SqlRepository<Brand>(_context);

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