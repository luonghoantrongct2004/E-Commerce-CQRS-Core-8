namespace E.API.Agregrates;

public static class ApiRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/[controller]";
    public const string IdRoute = "{id}";

    public static class Identity
    {
        public const string Base = "identity";
        public const string Login = $"{Base}/login";
        public const string Register = $"{Base}/register";
        public const string ById = $"{Base}/{{userId}}";
        public const string CurrentUser = $"{Base}/currentUser";
    }

    public static class Brand
    {
        public const string Base = "brand";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{brandId}}";
    }
}
