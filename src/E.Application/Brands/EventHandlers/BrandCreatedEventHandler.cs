using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.EventHandlers;

public class BrandCreatedEventHandler : INotificationHandler<BrandCreatedAndUpdateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public BrandCreatedEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(BrandCreatedAndUpdateEvent notification,
        CancellationToken cancellationToken)
    {
        var existingBrand = _readUnitOfWork.Brands.FirstOrDefaultAsync(
            b => b.Id == notification.BrandId);
        if (existingBrand.Result == null)
        {
            var brand = new Brand
            {
                Id = notification.BrandId,
                BrandName = notification.BrandName
            };

            await _readUnitOfWork.Brands.AddAsync(brand);
        }
    }
}