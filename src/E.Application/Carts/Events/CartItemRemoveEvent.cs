using E.Domain.Entities;
using MediatR;

namespace E.Application.Carts.Events;

public class CartItemRemoveEvent : BaseEntity, INotification
{
    public CartItemRemoveEvent(Guid id)
    {
        Id = id;
    }
}