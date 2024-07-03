using Microsoft.AspNetCore.Mvc.Versioning;

namespace E.API.Registrars.MVC;

public class MvcRegistar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
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
    }
}