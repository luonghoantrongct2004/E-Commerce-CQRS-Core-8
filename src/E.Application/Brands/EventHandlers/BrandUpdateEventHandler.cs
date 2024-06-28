using E.Application.Brands.Events;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.EventHandlers;

public class BrandUpdateEventHandler : INotificationHandler<BrandUpdateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public BrandUpdateEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(BrandUpdateEvent notification,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Brand>();
        var existingBrand = await _readUnitOfWork.Brands.FirstOrDefaultAsync(
            b => b.Id == notification.Id);
        if (existingBrand != null)
        {
            existingBrand.BrandName = notification.BrandName;

            await _readUnitOfWork.Brands.UpdateAsync(existingBrand.Id, existingBrand);
        }
        else
        {
            result.AddError(ErrorCode.NotFound,
                   string.Format(BrandErrorMessage.BrandNotFound, notification.Id));
        }
    }
}