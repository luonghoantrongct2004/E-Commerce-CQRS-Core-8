using E.Application.Coupons.Events;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.CouponServices;
using E.DAL.UoW;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.EventHandlers;

public class DisableCouponEventHandler
    : INotificationHandler<DisableCouponEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly CouponService _couponService;

    public DisableCouponEventHandler(IReadUnitOfWork readUnitOfWork,
        CouponService couponService)
    {
        _readUnitOfWork = readUnitOfWork;
        _couponService = couponService;
    }

    public async Task Handle(DisableCouponEvent notification, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Coupon>();
        try
        {
            var existingEntity = await _readUnitOfWork.Coupons.FirstOrDefaultAsync(
                b => b.Id == notification.Id);

            if (existingEntity != null)
            {
                _couponService.DisableCoupon(existingEntity);
                await _readUnitOfWork.Coupons.UpdateAsync(existingEntity.Id, existingEntity);
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