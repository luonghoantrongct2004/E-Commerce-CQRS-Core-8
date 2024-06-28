using MediatR;

namespace E.Domain.Entities.Products.Events;

public class ProductRemoveEvent: INotification
{
    public Guid ProductId { get; set; }

    public ProductRemoveEvent(Guid productId)
    {
        ProductId = productId;
    }
}