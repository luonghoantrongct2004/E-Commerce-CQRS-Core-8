using E.Domain.Entities.Carts;

namespace E.Application.Services.CartServices;

public class CartService
{
    private readonly CartValidationService _cartValidationService;

    public CartService(CartValidationService cartValidationService)
    {
        _cartValidationService = cartValidationService;
    }

    public CartDetails AddProductIntoCart(Guid UserId, Guid ProductId, int Quantity)
    {
        var objectToValidate = new CartDetails
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            ProductId = ProductId,
            Quantity = Quantity
        };
        _cartValidationService.ValidateAndThrow(objectToValidate);

        return objectToValidate;
    }

    public void UpdateCart(CartDetails cart, int quantity)
    {
        cart.Quantity = quantity;
        _cartValidationService.ValidateAndThrow(cart);
    }

    public void ApplyCoupon(CartDetails cart, Guid couponId)
    {
        cart.CouponId = couponId;
        _cartValidationService.ValidateAndThrow(cart);
    }

    public void DeleteProductInCart(CartDetails cart, Guid productId)
    {
        cart.ProductId = productId;
        _cartValidationService.ValidateAndThrow(cart);
    }
}