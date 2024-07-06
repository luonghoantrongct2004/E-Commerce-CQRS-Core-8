using E.Application.Carts.Events;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Carts;
using MediatR;

namespace E.Application.Carts.EventHandlers;

public class CartItemUpdateEventHandler : INotificationHandler<CartItemUpdateEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CartItemUpdateEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(CartItemUpdateEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<CartDetails>();
        try
        {
            var existProduct = await _readUnitOfWork.Carts.FirstOrDefaultAsync(
                c => c.ProductId == notification.ProductId);
            if (existProduct != null)
            {
                existProduct.Quantity = notification.Quantity;
                await _readUnitOfWork.Carts.UpdateAsync(existProduct.Id, existProduct);
            }
            else
            {
                result.AddError(ErrorCode.NotFound,
                       string.Format(CartErrorMessage.CartNotFound, notification.CartDetailId));
            }
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}