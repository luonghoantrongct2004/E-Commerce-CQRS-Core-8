using E.API.Contracts.Common;
using E.API.Registrars.RegistrarBase;
using E.Application.Brands.CommandHandlers;
using E.Application.Brands.Commands;
using E.Application.Categories.CommandHanlders;
using E.Application.Categories.Commands;
using E.Application.Models;
using E.Application.Products.CommandHandlers;
using E.Application.Products.Commands;
using E.Application.Products.EventHandlers;
using E.Application.Products.Queries;
using E.Application.Products.QueryHandlers;
using E.DAL.EventPublishers;
using E.DAL.Repository;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Categories.Events;
using E.Domain.Entities.Products;
using E.Domain.Entities.Products.Events;
using MediatR;

namespace E.API.Registrars.DependencyInjection;

public class DependencyInjection : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventPublisher, InMemoryEventPublisher>();
        builder.Services.AddScoped<IErrorResponseHandler, ErrorResponseHandler>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(MongoRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IReadUnitOfWork, ReadUnitOfWork>();

        builder.Services.AddTransient<INotificationHandler<ProductEvent>, ProductEventHandler>();
        builder.Services.AddTransient<IRequestHandler<GetAllProducts, OperationResult<IEnumerable<Product>>>, GetAllProductQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetProductById, OperationResult<Product>>, GetProductByIdQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateProductCommand, OperationResult<Product>>, CreateProductCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<DeleteProductCommand, OperationResult<Product>>, DeleteProductCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<UpdateProductCommand, OperationResult<Product>>, UpdateProductCommandHandler>();

        builder.Services.AddTransient<INotificationHandler<BrandCreatedEvent>, BrandCreatedEventHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateBrandCommand, OperationResult<Brand>>, CreateBrandCommandHandler>();

        builder.Services.AddTransient<INotificationHandler<CategoryCreateEvent>, CategoryCreateEventHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateCategoryCommand, OperationResult<Category>>, CategoryCreateCommandHandler>();
    }
}
