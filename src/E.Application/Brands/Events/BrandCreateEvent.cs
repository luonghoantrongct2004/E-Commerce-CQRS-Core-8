using MediatR;

namespace E.Domain.Entities.Brands.Events;

public class BrandCreateEvent : BaseEntity, INotification
{
    public string BrandName { get; }

    public BrandCreateEvent(Guid id, string brandName)
    {
        Id = id;
        BrandName = brandName;
    }
}