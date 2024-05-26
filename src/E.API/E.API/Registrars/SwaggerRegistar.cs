using E.API.Options;

namespace E.API.Registrars;

public class SwaggerRegistar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

        builder.Services.AddSwaggerGen();
    }
}