using MediatR;

namespace E.Domain.Entities.Categories.Events;

public class CategoryCreateEvent: BaseEntity, INotification
{
    public string CategoryName { get; set; }
    public CategoryCreateEvent(Guid id, string categoryName)
    {
        Id = id;
        CategoryName = categoryName;
    }
}