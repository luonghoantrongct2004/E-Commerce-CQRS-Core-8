using E.Application.Coupons.Commands;
using E.Application.Coupons.Events;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Services.CouponServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.CommandHandlers;

public class UpdateCouponCommandHandler
    : IRequestHandler<UpdateCouponCommand, OperationResult<Coupon>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly CouponService _couponService;

    public UpdateCouponCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, CouponService couponService)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _couponService = couponService;
    }

    public async Task<OperationResult<Coupon>> Handle(UpdateCouponCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Coupon>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var coupon = await _unitOfWork.Coupons.FirstOrDefaultAsync(
                c => c.Id == request.Id);

            if(coupon is null)
            {
                result.AddError(ErrorCode.NotFound,
                    CouponErrorMessage.CouponNotFound);
                return result;
            }

            _couponService.UpdateCoupon(coupon, request.CouponCode, request.DiscountAmount,
                request.MinAmount, request.ExpirationDate, request.UsageLimit);

            _unitOfWork.Coupons.Update(coupon);
            await _unitOfWork.CompleteAsync();

            var couponUpdateEvent = new UpdateCouponEvent(coupon.Id,coupon.CouponCode,
                coupon.DiscountAmount, coupon.MinAmount, coupon.CreatedDate,
                coupon.ExpirationDate, coupon.UsageLimit);
            await _eventPublisher.PublishAsync(couponUpdateEvent);

            await _unitOfWork.CommitAsync();

            result.Payload = coupon;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}