using E.Application.Brands.Commands;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.CommandHandlers;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, OperationResult<Brand>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }
    public async Task<OperationResult<Brand>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Brand>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var brand = await _unitOfWork.Brands.FirstOrDefaultAsync(
                b => b.Id == request.Id);
            if(brand is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(BrandErrorMessage.BrandNotFound, request.Id));
            }
            if(brand.Id != request.Id)
            {
                result.AddError(ErrorCode.PostDeleteNotPossible,
                    BrandErrorMessage.BrandDeleteNotPossible);
                return result;
            }
            brand.UpdateBrand(brandName : request.BrandName);
            var brandEvent = new BrandCreatedAndUpdateEvent(brand.Id, brand.BrandName);
            await _eventPublisher.PublishAsync(brandEvent);

            await _unitOfWork.CommitAsync();
            result.Payload = brand;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(ex.Message);
        }
        return result;
    }
}