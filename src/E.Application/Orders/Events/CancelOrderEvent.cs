using E.Domain.Entities.Orders;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Orders.Events;

public class CancelOrderEvent : INotification
{
    public Order Order { get; set; }
    public Product Product { get; set; }

    public CancelOrderEvent(Order order, Product product)
    {
        Order = order;
        Product = product;
    }
}