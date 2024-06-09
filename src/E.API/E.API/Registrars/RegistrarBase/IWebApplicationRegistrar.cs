namespace E.API.Registrars.RegistrarBase;

public interface IWebApplicationRegistrar : IRegistrar
{
    void RegisterPipelineComponents(WebApplication app);
}