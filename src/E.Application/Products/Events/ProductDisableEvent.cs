using MediatR;

namespace E.Domain.Entities.Products.Events;

public class ProductDisableEvent : BaseEntity, INotification
{
    public ProductDisableEvent(Guid id)
    {
        Id = id;
    }
}