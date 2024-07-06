using E.Domain.Entities.Orders;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Orders.Events;

public class ConfirmPurcharEvent : INotification
{
    public Order Order { get; set; }
    public Product Product { get; set; }

    public ConfirmPurcharEvent(Order order, Product product)
    {
        Order = order;
        Product = product;
    }
}