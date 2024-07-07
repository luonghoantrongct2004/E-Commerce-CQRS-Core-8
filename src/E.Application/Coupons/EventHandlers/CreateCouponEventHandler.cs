using E.Application.Coupons.Events;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Coupons.EventHandlers;

public class CreateCouponEventHandler : INotificationHandler<CreateCouponEvent>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CreateCouponEventHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task Handle(CreateCouponEvent notification,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Coupon>();
        try
        {
            var coupon = new Coupon
            {
                Id = notification.Id,
                DiscountAmount = notification.DiscountAmount,
                MinAmount = notification.MinAmount,
                CreatedDate = notification.CreatedDate,
                ExpirationDate = notification.ExpirationDate,
                UsageLimit = notification.UsageLimit,
                IsActive = true,
                DiscountPercentage = notification.DiscountPercentage,
                Type = notification.Type,
            };
            await _readUnitOfWork.Coupons.AddAsync(coupon);
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError,
                   ex.Message);
        }

    }
}