using E.API.Contracts.Common;
using E.API.Registrars.RegistrarGeneric;
using E.Application.Brands.CommandHandlers;
using E.Application.Brands.Commands;
using E.Application.Categories.CommandHanlders;
using E.Application.Categories.Commands;
using E.Application.Identity.CommandHandlers;
using E.Application.Identity.Commands;
using E.Application.Identity.Options;
using E.Application.Identity.Queries;
using E.Application.Identity.QueryHandlers;
using E.Application.Models;
using E.Application.Products.CommandHandlers;
using E.Application.Products.Commands;
using E.Application.Products.Queries;
using E.Application.Products.QueryHandlers;
using E.Application.Services;
using E.DAL.EventPublishers;
using E.DAL.Repository;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Products;
using E.Domain.Entities.Users.Dto;
using MediatR;

namespace E.API.Registrars.DependencyInjection;

public class DependencyInjection : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IdentityService>();

        builder.Services.AddScoped<IEventPublisher, InMemoryEventPublisher>();
        builder.Services.AddScoped<IErrorResponseHandler, ErrorResponseHandler>();
        builder.Services.AddTransient<IErrorResponseHandler, ErrorResponseHandler>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(MongoRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IReadUnitOfWork, ReadUnitOfWork>();
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

        builder.Services.AddTransient<IRequestHandler<GetAllProducts, OperationResult<IEnumerable<Product>>>, GetAllProductQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetProductById, OperationResult<Product>>, GetProductByIdQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateProductCommand, OperationResult<Product>>, CreateProductCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<RemoveProductCommand, OperationResult<Product>>, RemoveProductCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<UpdateProductCommand, OperationResult<Product>>, UpdateProductCommandHandler>();

        builder.Services.AddTransient<IRequestHandler<CreateBrandCommand, OperationResult<Brand>>, CreateBrandCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<UpdateBrandCommand, OperationResult<Brand>>, UpdateBrandCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<RemoveBrandCommand, OperationResult<bool>>, RemoveBrandCommandHandler>();

        builder.Services.AddTransient<IRequestHandler<CreateCategoryCommand, OperationResult<Category>>, CategoryCreateCommandHandler>();

        builder.Services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);
        builder.Services.AddTransient<IRequestHandler<RegisterUserCommand, OperationResult<IdentityUserDto>>, RegisterUserCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<GetCurrentUserQuery, OperationResult<IdentityUserDto>>, GetCurrentUserQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<LoginCommand, OperationResult<IdentityUserDto>>, LoginCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<RemoveUserCommand, OperationResult<bool>>, RemoveUserCommandHandler>();
    }
}