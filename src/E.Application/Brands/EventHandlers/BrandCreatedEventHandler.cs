using E.Application.Enums;
using E.Application.Models;
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
        var result = new OperationResult<Brand>();
        try
        {
            var brand = new Brand
            {
                Id = notification.Id,
                BrandName = notification.BrandName
            };

            await _readUnitOfWork.Brands.AddAsync(brand);
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}