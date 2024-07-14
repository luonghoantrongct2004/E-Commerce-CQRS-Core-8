using E.DAL;
using E.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace E.API.Registrars.Database;

public class DatabaseRegister : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSQL"));
        });

        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
        builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });
        builder.Services.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        builder.Services.AddSingleton<MongoDbContext>();
    }
}