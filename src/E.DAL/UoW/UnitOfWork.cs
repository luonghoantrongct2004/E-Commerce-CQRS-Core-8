using E.DAL.Repository;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Products;
using E.Domain.Models;

namespace E.DAL.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<Product>? _productRepository;
    private IRepository<Category>? _categoryRepository;
    private IRepository<Brand>? _brandRepository;
    public IRepository<Product> Products => _productRepository ??= new Repository<Product>(_context);
    public IRepository<Category> Categories => _categoryRepository ??= new Repository<Category>(_context);

    public IRepository<Brand> Brands => _brandRepository ??= new Repository<Brand>(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}