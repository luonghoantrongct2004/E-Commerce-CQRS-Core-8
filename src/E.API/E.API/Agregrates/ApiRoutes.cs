namespace E.API.Agregrates;

public class ApiRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/[controller]";
    public const string IdRoute = "{id}";
    public static class Identity
    {
        public const string Login = "login";
        public const string Register = "register";
        public const string IdentitybyId = "{userId}";
        public const string CurrentUser = "currentUser";
    }
}
