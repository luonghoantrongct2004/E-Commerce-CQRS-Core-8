using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.EventHandlers;

public class CategoryRemoveEventHandler : INotificationHandler<BrandRemoveEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CategoryRemoveEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork ?? throw new ArgumentNullException(nameof(readUnitOfWork));
    }

    public async Task Handle(BrandRemoveEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Brand>();
        try
        {
            var existingEntity = await _readUnitOfWork.Brands.FirstOrDefaultAsync(
                b => b.Id == notification.Id);

            if (existingEntity != null)
            {
                await _readUnitOfWork.Brands.RemoveAsync(existingEntity.Id);
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