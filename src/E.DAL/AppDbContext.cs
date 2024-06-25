﻿using E.Domain.Entities.Brand;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Comment;
using E.Domain.Entities.Products;
using E.Domain.Entities.Users;
using E.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<DomainUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<CartDetails>()
            .HasOne(cd => cd.User)
            .WithMany(u => u.CartDetails)
            .HasForeignKey(cd => cd.UserId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<Orderdetail>()
            .HasOne(od => od.Product)
            .WithMany(p => p.Orderdetails)
            .HasForeignKey(od => od.ProductId);

        modelBuilder.Entity<Coupon>()
            .Property(c => c.DiscountAmount)
            .HasColumnType("decimal(18,0)");

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasColumnType("decimal(18,0)");
    }
    public DbSet<DomainUser> Users { get; set; }
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