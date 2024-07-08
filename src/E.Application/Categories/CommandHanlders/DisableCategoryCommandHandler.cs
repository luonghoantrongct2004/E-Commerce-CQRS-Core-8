using E.Application.Categories.Commands;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.CategoryServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Categories.CommandHandlers;

public class DisableCategoryCommandHandler : IRequestHandler<DisableCategoryCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly CategoryService _categoryServices;

    public DisableCategoryCommandHandler(IUnitOfWork unitOfWork, 
        IEventPublisher eventPublisher, CategoryService categoryServices)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _categoryServices = categoryServices;
    }

    public async Task<OperationResult<bool>> Handle(DisableCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var category = await _unitOfWork.Categories.FirstOrDefaultAsync(
                b => b.Id == request.CategoryId);
            if (category is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(CategoryErrorMessage.CategoryNotFound, request.CategoryId));
                return result;
            }
            _categoryServices.DisableCategory(category);
            _unitOfWork.Categories.Update(category);
            var categoryEvent = new BrandDisableEvent(request.CategoryId);
            await _eventPublisher.PublishAsync(categoryEvent);

            await _unitOfWork.CommitAsync();

            result.Payload = true;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}