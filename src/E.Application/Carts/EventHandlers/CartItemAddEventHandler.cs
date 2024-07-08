using E.Application.Carts.Events;
using E.Application.Enums;
using E.Application.Identity;
using E.Application.Models;
using E.Application.Products;
using E.DAL.UoW;
using E.Domain.Entities.Carts;
using MediatR;

namespace E.Application.Carts.EventHandlers;

public class CartItemAddEventHandler : INotificationHandler<CartItemAddEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CartItemAddEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(CartItemAddEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<CartDetails>();
        try
        {
            var user = await _readUnitOfWork.Users.GetByIdAsync(notification.UserId);
            if (user is null)
            {
                result.AddError(ErrorCode.NotFound,
                    UserErrorMessage.UserNotFound(notification.Id));
            }

            var product = await _readUnitOfWork.Products.GetByIdAsync(notification.ProductId);
            if (product is null)
            {
                result.AddError(ErrorCode.NotFound,
                    ProductErrorMessage.ProductNotFound(notification.ProductId));
            }
            var cart = new CartDetails
            {
                Id = notification.Id,
                ProductId = notification.ProductId,
                UserId = notification.UserId,
                Quantity = notification.Quantity,
            };
            await _readUnitOfWork.Carts.AddAsync(cart);
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}