using E.API.Registrars.Swagger.Options;

namespace E.API.Registrars.Swagger;

public class SwaggerRegistar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
    }
}