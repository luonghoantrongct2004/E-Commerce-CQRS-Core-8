using E.Application.Carts;
using E.Application.Coupons.Events;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.CouponServices;
using E.DAL.UoW;
using E.Domain.Entities.Carts;
using E.Domain.Entities.Coupons;
using E.Domain.Enum;
using MediatR;

namespace E.Application.Coupons.EventHandlers;

public class ApplyCouponEventHandler : INotificationHandler<ApplyCouponEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly CouponService _couponService;

    public ApplyCouponEventHandler(IReadUnitOfWork readUnitOfWork,
        CouponService couponService)
    {
        _readUnitOfWork = readUnitOfWork;
        _couponService = couponService;
    }

    public async Task Handle(ApplyCouponEvent notification,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Coupon>();
        try
        {
            var cart = await _readUnitOfWork.Carts.FirstOrDefaultAsync(
                cd => cd.UserId == notification.UserId && cd.ProductId == notification.ProductId);

            if (cart is null)
            {
                result.AddError(ErrorCode.NotFound, CartErrorMessage.CartNotFound);
                return;
            }

            var coupon = await _readUnitOfWork.Coupons.GetByIdAsync(notification.Id);
            ApplyCouponToCart(cart, coupon);

            await _readUnitOfWork.Carts.UpdateAsync(cart.Id, cart);
            await _readUnitOfWork.Coupons.UpdateAsync(coupon.Id, coupon);
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }

    private void ApplyCouponToCart(CartDetails cart, Coupon coupon)
    {
        cart.CouponId = coupon.Id;
        cart.DiscountAmount = CalculateDiscount(cart, coupon);

        _couponService.ApplyCoupon(coupon);
    }

    private decimal CalculateDiscount(CartDetails cart, Coupon coupon)
    {
        decimal discount = 0;

        if (coupon.Type == CouponType.Percentage)
        {
            discount = cart.CartTotal * coupon.DiscountPercentage / 100;
        }
        else if (coupon.Type == CouponType.FixedAmount)
        {
            discount = coupon.DiscountAmount;
        }

        return Math.Min(discount, cart.CartTotal);
    }
}