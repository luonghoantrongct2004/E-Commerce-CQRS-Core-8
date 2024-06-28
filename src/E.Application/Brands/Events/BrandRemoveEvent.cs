using MediatR;

namespace E.Domain.Entities.Brands.Events;

public class BrandRemoveEvent : BaseEntity, INotification
{
    public BrandRemoveEvent(Guid id)
    {
        Id = id;
    }
}