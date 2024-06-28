using E.Domain.Entities;
using MediatR;

namespace E.Application.Categories.Events;

public class CategoryUpdateEvent : BaseEntity, INotification
{
    public string CategoryName { get; }

    public CategoryUpdateEvent(Guid id, string categoryName)
    {
        Id = id;
        CategoryName = categoryName;
    }
}