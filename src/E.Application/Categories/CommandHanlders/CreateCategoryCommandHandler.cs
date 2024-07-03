using E.Application.Categories.Commands;
using E.Application.Models;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Categories;
using E.Domain.Entities.Categories.Events;
using MediatR;

namespace E.Application.Categories.CommandHanlders;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, OperationResult<Category>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Category>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var category = Category.CreateCategory(request.CategoryName);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            var categoryCreateEvent = new CategoryCreateEvent(category.Id, category.CategoryName);
            await _eventPublisher.PublishAsync(categoryCreateEvent);

            await _unitOfWork.CommitAsync();

            result.Payload = category;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}