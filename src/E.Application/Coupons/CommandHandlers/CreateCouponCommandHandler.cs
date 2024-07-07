using E.Application.Coupons.Commands;
using E.Application.Coupons.Events;
using E.Application.Models;
using E.Application.Services.CouponServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.CommandHandlers;

public class CreateCouponCommandHandler
    : IRequestHandler<CreateCouponCommand, OperationResult<Coupon>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly CouponService _couponService;

    public CreateCouponCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, CouponService couponService)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _couponService = couponService;
    }

    public async Task<OperationResult<Coupon>> Handle(CreateCouponCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Coupon>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var coupon = _couponService.CreateCoupon(request.CouponCode,
                request.DiscountAmount, request.MinAmount, request.ExpirationDate,
                request.UsageLimit, request.DiscountPercentage, request.Type);

            await _unitOfWork.Coupons.AddAsync(coupon);
            await _unitOfWork.CompleteAsync();

            var couponCreateEvent = new CreateCouponEvent(coupon.Id, coupon.CouponCode,
                coupon.DiscountAmount, coupon.CreatedDate, coupon.MinAmount,
                coupon.ExpirationDate, coupon.UsageLimit, coupon.DiscountPercentage,
                coupon.Type);
            await _eventPublisher.PublishAsync(couponCreateEvent);

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