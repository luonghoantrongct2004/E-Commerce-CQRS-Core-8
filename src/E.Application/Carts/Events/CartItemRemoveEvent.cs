using MediatR;

namespace E.Application.Carts.Events;

public class CartItemRemoveEvent : INotification
{
    public Guid Id { get; set; }

    public CartItemRemoveEvent(Guid id)
    {
        Id = id;
    }
}