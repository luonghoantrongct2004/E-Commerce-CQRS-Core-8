using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.EventHandlers;

public class BrandUpdateEventHandler : INotificationHandler<BrandCreatedAndUpdateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public BrandUpdateEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(BrandCreatedAndUpdateEvent notification,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Brand>();
        var existingBrand = await _readUnitOfWork.Brands.FirstOrDefaultAsync(
            b => b.Id == notification.BrandId);
        if (existingBrand != null)
        {
            existingBrand.BrandName = notification.BrandName;

            await _readUnitOfWork.Brands.UpdateAsync(existingBrand.Id, existingBrand);
        }
        else
        {
            result.AddError(ErrorCode.NotFound,
                   string.Format(BrandErrorMessage.BrandNotFound, notification.BrandId));
        }
    }
}