using MediatR;

namespace E.Domain.Entities.Brands.Events;

public class BrandCreatedAndUpdateEvent: INotification
{
    public Guid BrandId { get; }
    public string BrandName { get; }

    public BrandCreatedAndUpdateEvent(Guid brandId, string brandName)
    {
        BrandId = brandId;
        BrandName = brandName;
    }
}