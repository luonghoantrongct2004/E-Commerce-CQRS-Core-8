using E.Domain.Entities.Orders;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Orders.Events;

public class CancelOrder : INotification
{
    public Order Order { get; set; }
    public Product Product { get; set; }

    public CancelOrder(Order order, Product product)
    {
        Order = order;
        Product = product;
    }
}