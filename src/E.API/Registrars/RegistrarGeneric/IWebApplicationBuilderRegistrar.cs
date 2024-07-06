namespace E.API.Registrars.RegistrarGeneric
{
    public interface IWebApplicationBuilderRegistrar : IRegistrar
    {
        void RegisterServices(WebApplicationBuilder builder);
    }
}