using E.Application.Carts;
using E.Application.Coupons.Commands;
using E.Application.Coupons.Events;
using E.Application.Enums;
using E.Application.Identity;
using E.Application.Models;
using E.Application.Products;
using E.Application.Services.CouponServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Carts;
using E.Domain.Entities.Coupons;
using E.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace E.Application.Coupons.CommandHandlers;

public class ApplyCouponCommandHandler : IRequestHandler<ApplyCouponCommand,
    OperationResult<Coupon>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly CouponService _couponService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplyCouponCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, CouponService couponService,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _couponService = couponService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResult<Coupon>> Handle(ApplyCouponCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Coupon>();

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("IdentityId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                result.AddError(ErrorCode.NotFound,
                    UserErrorMessage.TokenNotFound);
                return result;
            }

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                result.AddUnknownError("Invalid format in the token.");
                return result;
            }

            var cart = await _unitOfWork.Carts.FirstOrDefaultAsync(
                cd => cd.UserId == userId && cd.ProductId == request.ProductId);

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                result.AddError(ErrorCode.NotFound, UserErrorMessage.UserNotFound(userId));
                return result;
            }

            var validationResult = await ValidateRequestAsync(request, result, cart);
            if (validationResult != null)
            {
                return validationResult;
            }

            if (cart is null)
            {
                result.AddError(ErrorCode.NotFound, CartErrorMessage.CartNotFound);
                return result;
            }

            var coupon = await _unitOfWork.Coupons.GetByIdAsync(request.CouponId);
            ApplyCouponToCart(cart, coupon);

            _unitOfWork.Carts.Update(cart);
            _unitOfWork.Coupons.Update(coupon);

            await _unitOfWork.CompleteAsync();

            var applyCouponEvent = new ApplyCouponEvent(coupon.Id, cart.UserId, cart.ProductId);
            await _eventPublisher.PublishAsync(applyCouponEvent);

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

    private async Task<OperationResult<Coupon>> ValidateRequestAsync(
        ApplyCouponCommand request, OperationResult<Coupon> result, CartDetails cart)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            result.AddError(ErrorCode.NotFound, ProductErrorMessage.ProductNotFound(request.ProductId));
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
                CouponErrorMessage.CouponExpirationDate(coupon.CouponCode)));
            return result;
        }

        if (!coupon.IsActive)
        {
            result.AddError(ErrorCode.ValidationError, string.Format(
                CouponErrorMessage.CouponNotActive(coupon.CouponCode)));
            return result;
        }

        if (coupon.UsageLimit <= 0)
        {
            result.AddError(ErrorCode.ValidationError,
                CouponErrorMessage.CouponUsageLimitReached);
            return result;
        }

        if (cart.CartTotal < coupon.MinAmount)
        {
            throw new InvalidOperationException(CouponErrorMessage.
                TotalPriceLessThanMinimum(coupon.MinAmount));
        }
        if (cart.CouponId == coupon.Id)
        {
            result.AddError(ErrorCode.ValidationError,
                CouponErrorMessage.CouponAlreadyApplied);
            return result;
        }

        return null;
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