using E.API.Registrars.RegistrarBase;
using E.API.Registrars.Swagger.Options;

namespace E.API.Registrars.Swagger;

public class SwaggerRegistar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

        builder.Services.AddSwaggerGen();
    }
}