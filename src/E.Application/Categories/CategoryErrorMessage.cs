namespace E.Application.Categories;

public class CategoryErrorMessage
{
    public const string CategoryNotFound = "No Category found with ID {0}";
    public const string CategoryDeleteNotPossible = "Only the owner of a post can delete it";

    public const string CategoryUpdateNotPossible =
        "Category update not possible because it's not the post owner that initiates the update";
}