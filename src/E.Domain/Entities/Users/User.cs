using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace E.Domain.Entities.Users;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid UserProfileId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string IdentityId { get; set; }

    public BasicUser BasicInfo { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastModified { get; set; }

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