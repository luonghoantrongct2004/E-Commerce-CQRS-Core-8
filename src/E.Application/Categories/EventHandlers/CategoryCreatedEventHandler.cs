using E.DAL.UoW;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Categories.Events;
using MediatR;

namespace E.Application.Categories.EventHandlers;

public class CategoryCreatedEventHandler : INotificationHandler<CategoryCreateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CategoryCreatedEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(CategoryCreateEvent notification,
        CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = notification.Id,
            CategoryName = notification.CategoryName
        };

        await _readUnitOfWork.Categories.AddAsync(category);
    }
}