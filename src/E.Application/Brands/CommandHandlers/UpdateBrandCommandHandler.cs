using E.Application.Brands.Commands;
using E.Application.Brands.Events;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.BrandServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.CommandHandlers;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, OperationResult<Brand>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly BrandService _brandService;

    public UpdateBrandCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, BrandService brandService)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _brandService = brandService;
    }

    public async Task<OperationResult<Brand>> Handle(UpdateBrandCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Brand>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var brand = await _unitOfWork.Brands.FirstOrDefaultAsync(
                b => b.Id == request.Id);
            if (brand is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(BrandErrorMessage.BrandNotFound, request.Id));
            }
            _brandService.UpdateBrand(brand,brandName: request.BrandName);
            _unitOfWork.Brands.Update(brand);

            await _unitOfWork.CompleteAsync();

            var brandEvent = new BrandUpdateEvent(brand.Id, brand.BrandName);
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