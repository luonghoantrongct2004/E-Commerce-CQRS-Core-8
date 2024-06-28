using E.Domain.Entities;
using MediatR;

namespace E.Application.Brands.Events;

public class BrandUpdateEvent : BaseEntity, INotification
{
    public string BrandName { get; }

    public BrandUpdateEvent(Guid id, string brandName)
    {
        Id = id;
        BrandName = brandName;
    }
}