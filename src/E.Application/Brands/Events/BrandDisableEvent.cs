using MediatR;

namespace E.Domain.Entities.Brands.Events;

public class BrandDisableEvent : BaseEntity, INotification
{
    public BrandDisableEvent(Guid id)
    {
        Id = id;
    }
}