using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E.Domain.Entities.Users.Events;

public class UserUpdateEvent : INotification
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }
    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }

    public UserUpdateEvent(string username, string password, string fullName,
        string email, string? avatar, string? address, 
        string? currentCity)
    {
        Username = username;
        Password = password;
        FullName = fullName;
        Email = email;
        Avatar = avatar;
        Address = address;
        CurrentCity = currentCity;
    }
}   