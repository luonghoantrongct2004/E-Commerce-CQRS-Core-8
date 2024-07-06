namespace E.Application.Identity;

public class UserErrorMessage
{
    public static string UserNotFound(Guid userId) => $"No user found with Id {userId}";
    public const string TokenNotFound = "Please login again !"
}