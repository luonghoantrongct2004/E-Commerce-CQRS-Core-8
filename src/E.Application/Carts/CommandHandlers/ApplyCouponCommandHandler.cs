using E.Application.Carts.Commands;
using E.Application.Carts.Events;
using E.Application.Coupons;
using E.Application.Enums;
using E.Application.Identity;
using E.Application.Models;
using E.Application.Products;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Carts;
using E.Domain.Entities.Coupons;
using MediatR;

namespace E.Application.Carts.CommandHandlers;

public class ApplyCouponCommandHandler : IRequestHandler<ApplyCouponCommand,
    OperationResult<CartDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public ApplyCouponCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<CartDetails>> Handle(ApplyCouponCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<CartDetails>();

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var validationResult = await ValidateRequestAsync(request, result);
            if (validationResult != null)
            {
                return validationResult;
            }

            var cart = await _unitOfWork.Carts.FirstOrDefaultAsync(
                cd => cd.UserId == request.UserId && cd.ProductId == request.ProductId);

            if (cart is null)
            {
                result.AddError(ErrorCode.NotFound, CartErrorMessage.CartNotFound);
                return result;
            }

            var coupon = await _unitOfWork.Coupons.GetByIdAsync(request.CouponId);
            ApplyCouponToCart(cart, coupon);

            _unitOfWork.Carts.Update(cart);
            _unitOfWork.Coupons.Update(coupon);

            var applyCouponEvent = new ApplyCouponEvent(cart.UserId, cart.ProductId,
                cart.CouponId);
            await _eventPublisher.PublishAsync(applyCouponEvent);

            await _unitOfWork.CommitAsync();

            result.Payload = cart;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(e.Message);
        }

        return result;
    }

    private async Task<OperationResult<CartDetails>> ValidateRequestAsync(
        ApplyCouponCommand request,OperationResult<CartDetails> result)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            result.AddError(ErrorCode.NotFound, UserErrorMessage.UserNotFound);
            return result;
        }

        var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            result.AddError(ErrorCode.NotFound, ProductErrorMessage.ProductNotFound);
            return result;
        }

        var coupon = await _unitOfWork.Coupons.GetByIdAsync(request.CouponId);
        if (coupon == null)
        {
            result.AddError(ErrorCode.NotFound, CouponErrorMessage.CouponNotFound);
            return result;
        }

        if (coupon.IsExpired())
        {
            result.AddError(ErrorCode.ValidationError, string.Format(
                CouponErrorMessage.CouponExpirationDate, coupon.CouponCode));
            return result;
        }

        if (!coupon.IsActive)
        {
            result.AddError(ErrorCode.ValidationError, string.Format(
                CouponErrorMessage.CouponNotActive, coupon.CouponCode));
            return result;
        }

        if (coupon.UsageLimit <= 0)
        {
            result.AddError(ErrorCode.ValidationError, 
                CouponErrorMessage.CouponUsageLimitReached);
            return result;
        }

        if (coupon.MinAmount > request.CartTotal)
        {
            result.AddError(ErrorCode.ValidationError,
                string.Format(CouponErrorMessage.TotalPriceLessThanMinimum,
                coupon.MinAmount));
            return result;
        }

        return null;
    }

    private void ApplyCouponToCart(CartDetails cart, Coupon coupon)
    {
        if (cart.CouponId == coupon.Id)
        {
            throw new InvalidOperationException(
                CouponErrorMessage.CouponAlreadyApplied);
        }

        cart.ApplyCoupon(coupon.Id);
        coupon.UsageLimit -= 1;
    }
}
