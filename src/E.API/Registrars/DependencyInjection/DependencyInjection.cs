using E.Application.Services.UserServices;

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

        builder.Services.AddTransient<IRequestHandler<GetAllProducts,
            OperationResult<IEnumerable<Product>>>, GetAllProductQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetProduct,
            OperationResult<Product>>, GetProductByIdQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateProductCommand,
            OperationResult<Product>>, CreateProductCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<DisableProductCommand,
            OperationResult<Product>>, DisableProductCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<UpdateProductCommand,
            OperationResult<Product>>, UpdateProductCommandHandler>();

        builder.Services.AddTransient<IRequestHandler<GetBrandsQuery,
            OperationResult<IEnumerable<Brand>>>, GetBrandsQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetBrandQuery,
            OperationResult<Brand>>, GetBrandQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateBrandCommand,
            OperationResult<Brand>>, CreateBrandCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<UpdateBrandCommand,
            OperationResult<Brand>>, UpdateBrandCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<DisableBrandCommand,
            OperationResult<bool>>, DisableBrandCommandHandler>();

        builder.Services.AddTransient<IRequestHandler<GetCategoriesQuery,
            OperationResult<IEnumerable<Category>>>, GetCategoriesQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetCategoryQuery,
            OperationResult<Category>>, GetCategoryQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateCategoryCommand,
            OperationResult<Category>>, CreateCategoryCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<UpdateCategoryCommand,
            OperationResult<Category>>, UpdateCategoryCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<DisableCategoryCommand,
            OperationResult<bool>>, DisableCategoryCommandHandler>();

        builder.Services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);
        builder.Services.AddTransient<IRequestHandler<RegisterUserCommand,
            OperationResult<IdentityUserDto>>, RegisterUserCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<GetCurrentUserQuery,
            OperationResult<IdentityUserDto>>, GetCurrentUserQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<LoginCommand,
            OperationResult<IdentityUserDto>>, LoginCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<DisableUserCommand,
            OperationResult<bool>>, DisableUserCommandHandler>();

        builder.Services.AddTransient<IRequestHandler<GetCartsQuery,
            OperationResult<IEnumerable<CartDetails>>>, GetCartsQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CartItemAddCommand,
            OperationResult<CartDetails>>, CartItemAddCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<CartItemRemoveCommand,
            OperationResult<bool>>, CartItemRemoveCommandHandler>();
    }
}