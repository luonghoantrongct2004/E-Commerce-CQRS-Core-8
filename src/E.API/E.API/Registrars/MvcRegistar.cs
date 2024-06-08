using E.API.Contracts.Common;
using E.Application.Models;
using E.Application.Products.Queries;
using E.Application.Products.QueryHandlers;
using E.DAL.EventPublishers;
using E.DAL.Repository;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace E.API.Registrars;

public class MvcRegistar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        builder.Services.AddVersionedApiExplorer(config =>
        {
            config.GroupNameFormat = "'v'VVV";
            config.SubstituteApiVersionInUrl = true;
        });
        builder.Services.AddMediatR(typeof(Program));
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddScoped<IErrorResponseHandler, ErrorResponseHandler>();

        builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(MongoRepository<>));
        builder.Services.AddSingleton<IEventPublisher, InMemoryEventPublisher>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IReadUnitOfWork, ReadUnitOfWork>();
        builder.Services.AddTransient<IRequestHandler<GetAllProducts, OperationResult<IEnumerable<Product>>>, GetAllProductQueryHandler>();
    }
}