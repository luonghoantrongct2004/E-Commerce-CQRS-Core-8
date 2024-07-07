using E.Application.Enums;
using E.Application.Models;
using E.Application.Orders.Events;
using E.Application.Products;
using E.Application.Services.OrderServices;
using E.DAL.UoW;
using E.Domain.Entities.Orders;
using MediatR;

namespace E.Application.Orders.EventHandlers;

public class ConfirmPurcharEvenHandler : INotificationHandler<ConfirmPurcharEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly OrderService _orderService;

    public ConfirmPurcharEvenHandler(IReadUnitOfWork readUnitOfWork, OrderService orderService)
    {
        _readUnitOfWork = readUnitOfWork;
        _orderService = orderService;
    }

    public async Task Handle(ConfirmPurcharEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Order>();
        try
        {
            var order = await _readUnitOfWork.Orders.FirstOrDefaultAsync(
                o => o.UserId == notification.Order.UserId && o.Id == notification.Order.Id);
            if (order == null)
            {
                result.AddError(ErrorCode.NotFound,
                    OrderErrorMessage.OrderNotFound);
                return;
            }

            var orderDetail = order.OrderDetails.FirstOrDefault();
            if (orderDetail == null)
            {
                result.AddError(ErrorCode.NotFound,
                    OrderErrorMessage.OrderNotFound);
                return;
            }

            var productId = orderDetail.ProductId;
            var product = await _readUnitOfWork.Products.FirstOrDefaultAsync(
                p => p.Id == productId);
            if (product == null)
            {
                result.AddError(ErrorCode.NotFound,
                    ProductErrorMessage.ProductNotFound(notification.Product.Id));
                return;
            }
            _orderService.ConfirmOrder(order, product);
            await _readUnitOfWork.Orders.UpdateAsync(order.Id, order);
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}