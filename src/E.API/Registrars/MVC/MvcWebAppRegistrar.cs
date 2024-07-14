namespace E.API.Registrars.MVC;

public class MvcWebAppRegistrar : IWebApplicationRegistrar
{
    public void RegisterPipelineComponents(WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseCors("CorsPolicy");

        app.UseAuthorization();
        app.UseAuthentication();
        app.MapControllers();
    }
}