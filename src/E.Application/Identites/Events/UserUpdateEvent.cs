using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E.Application.Identity.Events;

public class UserUpdateEvent : INotification
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }

    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }

    public UserUpdateEvent(Guid userId, string username,
        string passwordHash, string fullName, DateTime createdDate,
        string? avatar, string? address, string? currentCity)
    {
        UserId = userId;
        Username = username;
        PasswordHash = passwordHash;
        FullName = fullName;
        CreatedDate = createdDate;
        Avatar = avatar;
        Address = address;
        CurrentCity = currentCity;
    }
}