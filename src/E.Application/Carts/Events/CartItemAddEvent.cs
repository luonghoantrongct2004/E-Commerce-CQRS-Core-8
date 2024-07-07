using E.Domain.Entities;
using MediatR;

namespace E.Application.Carts.Events;

public class CartItemAddEvent : BaseEntity, INotification
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public CartItemAddEvent( Guid userId, Guid productId, int quantity, Guid id)
    {
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
        Id = id;
    }
}