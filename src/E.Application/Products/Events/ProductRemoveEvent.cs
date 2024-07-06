using MediatR;

namespace E.Domain.Entities.Products.Events;

public class ProductRemoveEvent : BaseEntity, INotification
{
    public ProductRemoveEvent(Guid id)
    {
        Id = id;
    }
}