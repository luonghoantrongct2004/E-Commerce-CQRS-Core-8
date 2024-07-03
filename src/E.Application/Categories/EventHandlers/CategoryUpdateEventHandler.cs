using E.Application.Categories.Events;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.EventHandlers;

public class CategoryUpdateEventHandler : INotificationHandler<CategoryUpdateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CategoryUpdateEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(CategoryUpdateEvent notification,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Category>();
        try
        {
            var existingCategory = await _readUnitOfWork.Categories.FirstOrDefaultAsync(
                b => b.Id == notification.Id);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = notification.CategoryName;

                await _readUnitOfWork.Categories.UpdateAsync(existingCategory.Id, existingCategory);
            }
            else
            {
                result.AddError(ErrorCode.NotFound,
                       string.Format(CategoryErrorMessage.CategoryNotFound, notification.Id));
            }
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                 ex.Message);
        }
    }
}