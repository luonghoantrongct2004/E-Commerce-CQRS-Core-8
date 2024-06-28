using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Categories.Events;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.EventHandlers;

public class CategoryRemoveEventHandler : INotificationHandler<CategoryRemoveEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CategoryRemoveEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(CategoryRemoveEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Category>();
        var existingEntity = await _readUnitOfWork.Categories.FirstOrDefaultAsync(
            b => b.Id == notification.Id);

        if (existingEntity != null)
        {
            await _readUnitOfWork.Categories.RemoveAsync(existingEntity.Id);
        }
        else
        {
            result.AddError(ErrorCode.NotFound,
                   string.Format(CategoryErrorMessage.CategoryNotFound, notification.Id));
        }
    }
}