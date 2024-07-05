namespace E.Application.Products;

public class ProductErrorMessage
{
    public static string ProductNotFound(Guid id) => $"No product found with ID {id}";
    public const string ProductDeleteNotPossible = "Only the owner of a post can delete it";

    public const string ProductUpdateNotPossible =
        "Product update not possible because it's not the post owner that initiates the update";
    public static string ProductStoppedWorking(string name) => $"Sorry, {name} has stopped working";
}