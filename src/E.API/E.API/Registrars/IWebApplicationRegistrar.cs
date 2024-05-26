namespace E.API.Registrars;

public interface IWebApplicationRegistrar : IRegistrar
{
    void RegisterPipelineComponents(WebApplication app);
}