using E.Application.Carts.Events;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Products;
using E.DAL.UoW;
using E.Domain.Entities.Carts;
using MediatR;

namespace E.Application.Carts.EventHandlers;

public class CartRemoveEventHandler : INotificationHandler<CartItemRemoveEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CartRemoveEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(CartItemRemoveEvent notification,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<CartDetails>();
        try
        {
            var existingEntity = await _readUnitOfWork.Carts.FirstOrDefaultAsync(
                c => c.Id == notification.Id);
            if (existingEntity != null)
            {
                await _readUnitOfWork.Carts.RemoveAsync(existingEntity.Id);
            }
            else
            {
                result.AddError(ErrorCode.NotFound,
                       string.Format(ProductErrorMessage.ProductNotFound
                       , notification.Id));
            }
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}