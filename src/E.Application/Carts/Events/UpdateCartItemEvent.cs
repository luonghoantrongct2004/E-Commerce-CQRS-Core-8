using MediatR;

namespace E.Application.Carts.Events;

public class UpdateCartItemEvent : INotification
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public UpdateCartItemEvent(Guid userId, Guid productId, int quantity)
    {
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
    }
}