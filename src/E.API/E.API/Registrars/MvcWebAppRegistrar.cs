namespace E.API.Registrars;

public class MvcWebAppRegistrar : IWebApplicationRegistrar
{
    public void RegisterPipelineComponents(WebApplication app)
    {
        app.UseAuthorization();
        app.UseAuthentication();

        app.MapControllers();
    }
}