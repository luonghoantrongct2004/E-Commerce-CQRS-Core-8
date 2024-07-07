using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.BrandServices;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.EventHandlers;

public class CategoryRemoveEventHandler : INotificationHandler<BrandDisableEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly BrandService _brandService;

    public CategoryRemoveEventHandler(IReadUnitOfWork readUnitOfWork, BrandService brandService)
    {
        _readUnitOfWork = readUnitOfWork;
        _brandService = brandService;
    }

    public async Task Handle(BrandDisableEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Brand>();
        try
        {
            var existingEntity = await _readUnitOfWork.Brands.FirstOrDefaultAsync(
                b => b.Id == notification.Id);

            if (existingEntity != null)
            {
                _brandService.DisableBrand(existingEntity);
                await _readUnitOfWork.Brands.UpdateAsync(existingEntity.Id, existingEntity);
            }
            else
            {
                result.AddError(ErrorCode.NotFound,
                       string.Format(BrandErrorMessage.BrandNotFound, notification.Id));
            }
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}