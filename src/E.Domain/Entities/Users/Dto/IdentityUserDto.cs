using System.ComponentModel.DataAnnotations;

namespace E.Domain.Entities.Users.Dto;

public class IdentityUserDto
{
    public string UserName { get; set; }
    public string FullName { get; set; }
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }
    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}