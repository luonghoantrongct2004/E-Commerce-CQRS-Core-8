namespace E.Application.Brands;

public class BrandErrorMessage
{
    public const string BrandNotFound = "No Brand found with ID {0}";
    public const string BrandDeleteNotPossible = "Only the owner of a post can delete it";

    public const string BrandUpdateNotPossible =
        "Brand update not possible because it's not the post owner that initiates the update";
}