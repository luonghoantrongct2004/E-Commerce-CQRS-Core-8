using MediatR;
using MongoDB.Bson.Serialization.Attributes;

namespace E.Domain.Entities.Brands.Events;

public class BrandRemoveEvent : INotification
{
    [BsonId]
    public Guid BrandId;

    public BrandRemoveEvent(Guid brandId)
    {
        BrandId = brandId;
    }
}