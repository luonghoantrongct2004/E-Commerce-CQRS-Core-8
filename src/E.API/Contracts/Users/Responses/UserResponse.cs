using System.ComponentModel.DataAnnotations;

namespace E.API.Contracts.Users.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }

    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }
}