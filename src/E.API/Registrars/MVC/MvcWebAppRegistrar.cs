using E.API.Registrars.RegistrarGeneric;

namespace E.API.Registrars.MVC;

public class MvcWebAppRegistrar : IWebApplicationRegistrar
{
    public void RegisterPipelineComponents(WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseAuthentication();
        app.MapControllers();
    }
}