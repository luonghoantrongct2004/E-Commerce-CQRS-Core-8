using E.Application.Coupons.Commands;
using E.Application.Coupons.Events;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.CouponServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using MediatR;

namespace E.Application.Coupons.CommandHandlers;

public class DisableCouponCommandHandler
    : IRequestHandler<DisableCouponCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly CouponService _couponService;

    public DisableCouponCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, CouponService couponService)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _couponService = couponService;
    }

    public async Task<OperationResult<bool>> Handle(DisableCouponCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var coupon = await _unitOfWork.Coupons.FirstOrDefaultAsync(
                c => c.Id == request.Id);

            if (coupon is null)
            {
                result.AddError(ErrorCode.NotFound,
                    CouponErrorMessage.CouponNotFound);
                return result;
            }
            _couponService.DisableCoupon(coupon);
            _unitOfWork.Coupons.Update(coupon);

            var couponDisableEvent = new DisableCouponEvent(coupon.Id);
            await _eventPublisher.PublishAsync(couponDisableEvent);

            await _unitOfWork.CommitAsync();

            result.Payload = true;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}