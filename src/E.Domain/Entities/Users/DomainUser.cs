using E.Domain.Entities.Carts;
using E.Domain.Entities.Orders;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E.Domain.Entities.Users;

public class DomainUser : IdentityUser<Guid>
{
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }

    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<CartDetails> CartDetails { get; set; } = new List<CartDetails>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}