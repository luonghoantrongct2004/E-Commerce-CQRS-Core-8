using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace E.Domain.Entities.Users.Events;

public class UserRegisterEvent : INotification
{
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }

    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }

    public UserRegisterEvent(Guid userId, string username,
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