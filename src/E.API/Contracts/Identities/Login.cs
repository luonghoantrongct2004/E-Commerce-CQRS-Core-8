using System.ComponentModel.DataAnnotations;

namespace E.API.Contracts.Identities;

public class Login
{
    [EmailAddress]
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
