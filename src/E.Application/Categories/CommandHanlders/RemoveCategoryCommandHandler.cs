using E.Application.Categories.Commands;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Categories.CommandHandlers;

public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public RemoveCategoryCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<bool>> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
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
            category.DeleteCategory(categoryId: category.Id);
            _unitOfWork.Categories.Remove(category);
            var categoryEvent = new BrandRemoveEvent(request.CategoryId);
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