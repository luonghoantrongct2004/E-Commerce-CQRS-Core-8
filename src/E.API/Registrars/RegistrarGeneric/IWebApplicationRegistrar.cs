namespace E.API.Registrars.RegistrarGeneric;

public interface IWebApplicationRegistrar : IRegistrar
{
    void RegisterPipelineComponents(WebApplication app);
}