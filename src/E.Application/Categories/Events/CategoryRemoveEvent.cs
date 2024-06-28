using MediatR;

namespace E.Domain.Entities.Categories.Events;

public class CategoryRemoveEvent : BaseEntity, INotification
{
    public CategoryRemoveEvent(Guid id)
    {
        Id = id;
    }
}