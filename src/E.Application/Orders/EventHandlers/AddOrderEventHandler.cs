using E.Application.Enums;
using E.Application.Models;
using E.Application.Orders.Events;
using E.DAL.UoW;
using E.Domain.Entities.Orders;
using MediatR;

namespace E.Application.Orders.EventHandlers;

public class AddOrderEventHandler : INotificationHandler<AddOrderEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public AddOrderEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(AddOrderEvent notification,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Order>();
        try
        {
            var order = new Order
            {
                Id = notification.Id,
                UserId = notification.UserId,
                OrderDate = notification.OrderDate,
                PaymentDate = notification.PaymentDate,
                Note = notification.Note,
                PaymentMethod = notification.PaymentMethod,
                ContactPhone = notification.ContactPhone,
                TotalPrice = notification.TotalPrice,
                OrderDetails = notification.OrderDetails,
                StatusString = notification.Status
            };
            await _readUnitOfWork.Orders.AddAsync(order);
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}