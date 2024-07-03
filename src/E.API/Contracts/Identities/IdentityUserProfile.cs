namespace E.API.Contracts.Identities;

public class IdentityUser
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string CurrentCity { get; set; }
    public string Token { get; set; }
}