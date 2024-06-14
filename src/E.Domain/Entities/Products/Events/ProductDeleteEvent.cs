using MediatR;

namespace E.Domain.Entities.Products.Events;

public class ProductDeleteEvent: INotification
{
    public Guid ProductId { get; set; }

    public ProductDeleteEvent(Guid productId)
    {
        ProductId = productId;
    }
}