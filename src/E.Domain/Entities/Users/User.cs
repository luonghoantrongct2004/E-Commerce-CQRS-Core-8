namespace E.Domain.Entities.Users;

public class User
{
    public Guid UserProfileId { get; private set; }
    public string IdentityId { get; private set; }
    public BasicUser BasicInfo { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime LastModified { get; private set; }

    public static User CreateUser(string identityId, BasicUser basicUser)
    {
        return new User
        {
            IdentityId = identityId,
            BasicInfo = basicUser,
            DateCreated = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        };
    }
    public void UpdateBasicUser(BasicUser newInfo)
    {
        BasicInfo = newInfo;
        LastModified = DateTime.UtcNow;
    }
}