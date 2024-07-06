using E.Application.Enums;
using E.Application.Models;
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
        var result = new OperationResult<Category>();
        try
        {
            var category = new Category
            {
                Id = notification.Id,
                CategoryName = notification.CategoryName
            };

            await _readUnitOfWork.Categories.AddAsync(category);
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}