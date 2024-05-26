using E.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions opt) : base(opt)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    public DbSet<Orderdetail> Orderdetails { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<CartDetails> CartDetails { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Introduction> Introductions { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<E.Domain.Models.Introduction> Introduction { get; set; } = default!;
}