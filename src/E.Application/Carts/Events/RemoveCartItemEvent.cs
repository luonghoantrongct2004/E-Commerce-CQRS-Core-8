using MediatR;

namespace E.Application.Carts.Events;

public class RemoveCartItemEvent : INotification
{
    public Guid CartDetailsId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
}