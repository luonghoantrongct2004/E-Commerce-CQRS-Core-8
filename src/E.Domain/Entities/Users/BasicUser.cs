using E.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E.Domain.Entities.Users;

public class BasicUser:IdentityUser<Guid>
{
    [Required(ErrorMessage = "Full name is required")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }
    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }
    public ICollection<CartDetails> CartDetails { get; set; } = new List<CartDetails>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
