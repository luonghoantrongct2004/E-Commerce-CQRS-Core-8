namespace E.API.Registrars;

public class MvcWebAppRegistrar : IWebApplicationRegistrar
{
    public void RegisterPiplelineComponents(WebApplication app)
    {
        app.UseAuthorization();

        app.MapControllers();
    }
}