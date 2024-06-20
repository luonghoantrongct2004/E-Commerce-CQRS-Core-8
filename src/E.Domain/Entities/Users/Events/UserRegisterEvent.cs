using MediatR;

namespace E.Domain.Entities.Users.Events;

public class UserRegisterEvent : INotification
{
    public Guid UserProfileId { get; private set; }
    public string IdentityId { get; private set; }
    public BasicUser BasicInfo { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime LastModified { get; private set; }

    public UserRegisterEvent(Guid userProfileId, string identityId, BasicUser basicInfo, DateTime dateCreated, DateTime lastModified)
    {
        UserProfileId = userProfileId;
        IdentityId = identityId;
        BasicInfo = basicInfo;
        DateCreated = dateCreated;
        LastModified = lastModified;
    }
}