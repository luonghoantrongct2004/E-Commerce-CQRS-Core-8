namespace E.Application.Products;

public class ProductErrorMessage
{
    public const string ProductNotFound = "No product found with ID {0}";
    public const string ProductDeleteNotPossible = "Only the owner of a post can delete it";

    public const string ProductUpdateNotPossible =
        "Product update not possible because it's not the post owner that initiates the update";
}