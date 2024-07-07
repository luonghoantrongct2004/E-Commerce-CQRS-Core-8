using E.Application.Brands.Commands;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.BrandServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.CommandHandlers;

public class DisableBrandCommandHandler : IRequestHandler<DisableBrandCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly BrandService _brandService;

    public DisableBrandCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, BrandService brandService)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _brandService = brandService;
    }

    public async Task<OperationResult<bool>> Handle(DisableBrandCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var brand = await _unitOfWork.Brands.FirstOrDefaultAsync(
                b => b.Id == request.BrandId);
            if (brand is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(BrandErrorMessage.BrandNotFound, request.BrandId));
                return result;
            }
            _brandService.DisableBrand(brand);
            _unitOfWork.Brands.Update(brand);
            var brandDeleteEvent = new BrandDisableEvent(request.BrandId);
            await _eventPublisher.PublishAsync(brandDeleteEvent);

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