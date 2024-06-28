using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.EventHandlers;

public class CategoryCreatedEventHandler : INotificationHandler<BrandCreateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CategoryCreatedEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(BrandCreateEvent notification,
        CancellationToken cancellationToken)
    {
        var brand = new Brand
        {
            Id = notification.Id,
            BrandName = notification.BrandName
        };

        await _readUnitOfWork.Brands.AddAsync(brand);
    }
}