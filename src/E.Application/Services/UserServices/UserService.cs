using E.Domain.Entities.Users;

namespace E.Application.Services.UserServices;

public class UserService
{
    private readonly UserValidationService _validationService;

    public UserService(UserValidationService validationService)
    {
        _validationService = validationService;
    }

    public DomainUser CreateBasicInfo(string fullName, string avatar,
    string address, string currentCity)
    {
        var objectToValidate = new DomainUser
        {
            FullName = fullName,
            Avatar = avatar,
            Address = address,
            CurrentCity = currentCity,
            CreatedDate = DateTime.Now,
        };
        _validationService.ValidateAndThrow(objectToValidate);

        return objectToValidate;
    }

    public void UpdateBasicInfo(DomainUser user,
        string username, string password, string fullName, string avatar,
    string address, string currentCity)
    {
        user.Email = username;
        user.UserName = username;
        user.PasswordHash = password;
        user.FullName = fullName;
        user.Avatar = avatar;
        user.Address = address;
        user.CurrentCity = currentCity;

        _validationService.ValidateAndThrow(user);
    }

    public void DisableUser(DomainUser user)
    {
        user.IsActive = false;
        _validationService.ValidateAndThrow(user);
    }
    public void DisableUserMongo(UserMongo user)
    {
        user.IsActive = false;
    }
}