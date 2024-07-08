using E.Application.Categories.Commands;
using E.Application.Categories.Events;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.CategoryServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.CommandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, OperationResult<Category>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly CategoryService _categoryServices;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, CategoryService categoryServices)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _categoryServices = categoryServices;
    }

    public async Task<OperationResult<Category>> Handle(UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Category>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var category = await _unitOfWork.Categories.FirstOrDefaultAsync(
                b => b.Id == request.Id);
            if (category is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(CategoryErrorMessage.CategoryNotFound, request.Id));
            }
            _categoryServices.UpdateCategory(category, categoryName: request.CategoryName);
            _unitOfWork.Categories.Update(category);

            await _unitOfWork.CompleteAsync();
            var brandEvent = new CategoryUpdateEvent(category.Id, category.CategoryName);
            await _eventPublisher.PublishAsync(brandEvent);

            await _unitOfWork.CommitAsync();
            result.Payload = category;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(ex.Message);
        }
        return result;
    }
}