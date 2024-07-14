namespace E.API.Contracts;

public static class ApiRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/[controller]";
    public const string IdRoute = "{id}";

    public static class Identity
    {
        public const string Login = $"login";
        public const string Logout = $"logout";
        public const string Register = $"register";
        public const string ById = $"{{userId}}";
        public const string CurrentUser = $"currentUser";
    }

    public static class Brand
    {
        public const string Create = $"";
        public const string Update = $"{{brandId}}";
        public const string Remove = $"{{brandId}}";
    }
    public static class Category
    {
        public const string Create = $"";
        public const string Update = $"{{categoryId}}";
        public const string Remove = $"{{categoryId}}";
    }

    public static class Product
    {
        public const string Create = $"";
        public const string Update = $"{{productId}}";
        public const string Remove = $"{{productId}}";
    }

    public static class Cart
    {
        public const string Gets = $"{{userId}}";
        public const string Add = $"{{userId}}/{{productId}}";
        public const string Remove = $"{{cartId}}";
    }
    public static class Order
    {
        public const string Get = $"{{orderId}}";
        public const string Gets = $"orders";
        public const string Add = $"";
        public const string CancelOrder = $"{{orderId}}/{{productId}}";
        public const string ConfirmOrder = $"{{orderId}}/{{productId}}";
    }
    public static class Coupon
    {
        public const string Get = $"{{couponId}}";
        public const string Gets = $"{{couponId}}";
        public const string ApplyCoupon = $"{{productId}}/{{couponId}}";
        public const string CreateCoupon = $"";
        public const string DisableCoupon = $"{{couponId}}";
        public const string UpdateCoupon = $"{{couponId}}";
    }
}