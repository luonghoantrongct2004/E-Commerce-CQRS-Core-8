using MediatR;

namespace E.Application.Carts.Events;

public class CartItemAddEvent : INotification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public CartItemAddEvent(Guid Id, Guid userId, Guid productId, int quantity)
    {
        Id = Id;
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
    }
}