using E.DAL.UoW;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Categories.Events;
using MediatR;

namespace E.Application.Categories.CommandHanlders;

public class CategoryCreateEventHandler : INotificationHandler<CategoryCreateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CategoryCreateEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(CategoryCreateEvent notification, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = notification.CategoryId,
            CategoryName = notification.CategoryName,
        };
        await _readUnitOfWork.Categories.AddAsync(category);
    }
}