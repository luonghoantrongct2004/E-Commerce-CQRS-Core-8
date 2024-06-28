using E.Application.Brands.Commands;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.CommandHandlers;

public class RemoveBrandCommandHandler : IRequestHandler<RemoveBrandCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public RemoveBrandCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<bool>> Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var brand = await _unitOfWork.Brands.FirstOrDefaultAsync(
                b => b.Id == request.BrandId);
            if(brand is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(BrandErrorMessage.BrandNotFound, request.BrandId));
                return result;
            }
            _unitOfWork.Brands.Remove(brand);
            var brandEvent = new BrandRemoveEvent(request.BrandId);
            await _eventPublisher.PublishAsync(brandEvent);

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