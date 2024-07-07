using MediatR;

namespace E.Domain.Entities.Categories.Events;

public class CategoryDisableEvent : BaseEntity, INotification
{
    public CategoryDisableEvent(Guid id)
    {
        Id = id;
    }
}