using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.CategoryServices;
using E.DAL.UoW;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Categories.Events;
using MediatR;

namespace E.Application.Categories.EventHandlers;

public class CategoryRemoveEventHandler : INotificationHandler<CategoryRemoveEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly CategoryServices _categoryServices;

    public CategoryRemoveEventHandler(IReadUnitOfWork readUnitOfWork, 
        CategoryServices categoryServices)
    {
        _readUnitOfWork = readUnitOfWork;
        _categoryServices = categoryServices;
    }

    public async Task Handle(CategoryRemoveEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Category>();
        try
        {
            var existingEntity = await _readUnitOfWork.Categories.FirstOrDefaultAsync(
                b => b.Id == notification.Id);

            if (existingEntity != null)
            {
                _categoryServices.DisableCategory(existingEntity);
                await _readUnitOfWork.Categories.RemoveAsync(existingEntity.Id);
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