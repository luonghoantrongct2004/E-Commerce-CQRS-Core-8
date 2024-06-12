using MediatR;

namespace E.Domain.Entities.Brands.Events;

public class BrandCreatedEvent: INotification
{
    public Guid BrandId { get; }
    public string BrandName { get; }

    public BrandCreatedEvent(Guid brandId, string brandName)
    {
        BrandId = brandId;
        BrandName = brandName;
    }
}