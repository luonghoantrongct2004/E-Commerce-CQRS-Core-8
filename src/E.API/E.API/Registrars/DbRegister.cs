using Microsoft.EntityFrameworkCore;

namespace E.API.Registrars;

public class DbRegister : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
        });
    }
}
