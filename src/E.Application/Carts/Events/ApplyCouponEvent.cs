using MediatR;

namespace E.Application.Carts.Events;

public class ApplyCouponEvent : INotification
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public Guid CouponId { get; set; }

    public ApplyCouponEvent(Guid userId, Guid productId, Guid couponId)
    {
        UserId = userId;
        ProductId = productId;
        CouponId = couponId;
    }
}