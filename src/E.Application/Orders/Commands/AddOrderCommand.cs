using E.Application.Models;
using E.Domain.Entities.Orders;
using MediatR;

namespace E.Application.Orders.Commands;

public class AddOrderCommand : IRequest<OperationResult<Order>>
{
    public string? Note { get; set; }
    public string PaymentMethod { get; set; }
    public string ContactPhone { get; set; }

    public decimal TotalPrice { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}