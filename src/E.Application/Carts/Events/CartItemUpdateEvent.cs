using MediatR;

namespace E.Application.Carts.Events;

public class CartItemUpdateEvent : INotification
{
    public Guid CartDetailId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public CartItemUpdateEvent(Guid cartDetailId, Guid userId, Guid productId, int quantity)
    {
        CartDetailId = cartDetailId;
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
    }
}