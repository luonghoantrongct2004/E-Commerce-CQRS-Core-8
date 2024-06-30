using MediatR;

namespace E.Application.Carts.Events;

public class AddCartItemEvent : INotification
{
    public Guid CartDetailsId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public AddCartItemEvent(Guid cartDetailsId, Guid userId, Guid productId, int quantity)
    {
        CartDetailsId = cartDetailsId;
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
    }
}