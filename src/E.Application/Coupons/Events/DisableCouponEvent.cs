using E.Domain.Entities;
using MediatR;

namespace E.Application.Coupons.Events;

public class DisableCouponEvent : BaseEntity, INotification
{
    public DisableCouponEvent(Guid id)
    {
        Id = id;
    }
}