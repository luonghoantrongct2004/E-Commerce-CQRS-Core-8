using E.Domain.Entities;
using MediatR;

namespace E.Application.Coupons.Events;

public class ApplyCouponEvent : BaseEntity, INotification
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    public ApplyCouponEvent(Guid couponId,Guid userId, Guid productId)
    {
        Id = couponId;
        UserId = userId;
        ProductId = productId;
    }
}