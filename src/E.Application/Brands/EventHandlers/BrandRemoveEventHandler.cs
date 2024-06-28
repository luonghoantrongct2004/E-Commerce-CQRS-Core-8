using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E.Application.Brands.EventHandlers;

public class BrandRemoveEventHandler : INotificationHandler<BrandRemoveEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public BrandRemoveEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork ?? throw new ArgumentNullException(nameof(readUnitOfWork));
    }

    public async Task Handle(BrandRemoveEvent notification, CancellationToken cancellationToken)
    {
            var existingBrand = await _readUnitOfWork.Brands.FirstOrDefaultAsync(
                b => b.Id == notification.BrandId);

            if (existingBrand != null)
            {
                await _readUnitOfWork.Brands.RemoveAsync(existingBrand.Id);
            }
    }
}
