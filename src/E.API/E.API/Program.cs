using E.API.Extensions;
using E.DAL;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices(typeof(Program));

var app = builder.Build();
app.RegisterPipelineComponents(typeof(Program));

// Initialize SQL Server
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying SQL.");
    }
}

// Initialize MongoDB
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var mongoClient = services.GetRequiredService<IMongoClient>();
        var database = mongoClient.GetDatabase("E-Ecormmerce");
        var mongoDbInitializer = services.GetRequiredService<MongoDbInitializer>();
        mongoDbInitializer.Initialize();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing MongoDB.");
    }
}
app.Run();