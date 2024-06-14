using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.CommandHandlers;

public class BrandCreatedEventHandler : INotificationHandler<BrandCreatedEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public BrandCreatedEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(BrandCreatedEvent notification, CancellationToken cancellationToken)
    {
        var brand = new Brand
        {
            BrandId = notification.BrandId,
            BrandName = notification.BrandName
        };

        await _readUnitOfWork.Brands.AddAsync(brand);
    }
}