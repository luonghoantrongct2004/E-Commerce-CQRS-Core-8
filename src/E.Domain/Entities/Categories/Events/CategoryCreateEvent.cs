using MediatR;

namespace E.Domain.Entities.Categories.Events;

public class CategoryCreateEvent: INotification
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public CategoryCreateEvent(Guid categoryId, string categoryName)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
    }
}