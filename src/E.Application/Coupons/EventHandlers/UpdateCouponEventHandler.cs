using E.Application.Coupons.Events;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.CouponServices;
using E.DAL.UoW;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.EventHandlers;

public class UpdateCouponEventHandler
    : INotificationHandler<UpdateCouponEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly CouponService _couponService;

    public UpdateCouponEventHandler(IReadUnitOfWork readUnitOfWork,
        CouponService couponService)
    {
        _readUnitOfWork = readUnitOfWork;
        _couponService = couponService;
    }

    public async Task Handle(UpdateCouponEvent notification,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Coupon>();
        try
        {
            var existingCoupon = await _readUnitOfWork.Coupons.FirstOrDefaultAsync(
                b => b.Id == notification.Id);
            if (existingCoupon != null)
            {
                _couponService.UpdateCoupon(existingCoupon, notification.CouponCode,
                    notification.DiscountAmount, notification.MinAmount,
                    notification.ExpirationDate, notification.UsageLimit);

                await _readUnitOfWork.Coupons.UpdateAsync(existingCoupon.Id, existingCoupon);
            }
            else
            {
                result.AddError(ErrorCode.NotFound,
                       string.Format(CouponErrorMessage.CouponNotFound, notification.Id));
            }
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }
    }
}