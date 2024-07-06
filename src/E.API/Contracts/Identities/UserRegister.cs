using System.ComponentModel.DataAnnotations;

namespace E.API.Contracts.Identities;

public class UserRegister
{
    [Required]
    [EmailAddress]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }

    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }
}