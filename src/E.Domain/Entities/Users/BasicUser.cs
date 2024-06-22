using E.Domain.Entities.Users.UserValidators;
using E.Domain.Exceptions;
using E.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E.Domain.Entities.Users;

public class BasicUser : IdentityUser<Guid>
{
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }

    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }
    public ICollection<CartDetails> CartDetails { get; set; } = new List<CartDetails>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public static BasicUser CreateBasicInfo(string fullName, string avatar,
    string address, string currentCity)
    {
        var validator = new UserValidator();
        var objectToValidate = new BasicUser
        {
            FullName = fullName,
            Avatar = avatar,
            Address = address,
            CurrentCity = currentCity,
            CreatedDate = DateTime.Now,
        };
        var validationResult = validator.Validate(objectToValidate);
        if (validationResult.IsValid) return objectToValidate;
        var exception = new UserInvalidException($"{validationResult}");
        foreach (var error in validationResult.Errors)
        {
            exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
        }
        throw exception;
    }
    public void UpdateBasicInfo(string username,string password,string fullName, string avatar,
    string address, string currentCity)
    {
        UserName = username;
        PasswordHash = password;
        FullName = fullName;
        Avatar = avatar;
        Address = address;
        CurrentCity = currentCity;

        var validator = new UserValidator();
        var validationResult = validator.Validate(this);
        var exception = new UserInvalidException($"{validationResult}");
        foreach (var error in validationResult.Errors)
        {
            exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
        }
        throw exception;
    }
}