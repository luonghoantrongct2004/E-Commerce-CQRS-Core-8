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
        public const string Remove = $"{Base}/{{brandId}}";
    }
    public static class Category
    {
        public const string Base = "category";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{categoryId}}";
        public const string Remove = $"{Base}/{{categoryId}}";
    }

    public static class Product
    {
        public const string Base = "product";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{productId}}";
        public const string Remove = $"{Base}/{{productId}}";
    }

    public static class Cart
    {
        public const string Base = "cart";
        public const string Gets = $"{Base}/{{userId}}";
        public const string Add = $"{Base}/{{userId}}/{{productId}}";
        public const string Remove = $"{Base}/{{cartId}}";
    }
}