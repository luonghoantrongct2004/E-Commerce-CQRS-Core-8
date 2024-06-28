using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace E.Domain.Entities;

public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
}