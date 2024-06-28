using MediatR;

namespace E.Domain.Entities.Brands.Events;

public class BrandRemoveEvent : INotification
{
    public Guid BrandId;

    public BrandRemoveEvent(Guid brandId)
    {
        BrandId = brandId;
    }
}