using E.Domain.Entities.Products;
using E.Domain.Entities.Users;
using E.Domain.Entities.Coupons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E.Domain.Entities.Carts.CartValidators;
using E.Domain.Exceptions;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace E.Domain.Entities.Carts;

public class CartDetails : BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();

    [BsonRepresentation(BsonType.String)]
    public Guid? CouponId { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public decimal CartTotal
    {
        get
        {
            var discount = Coupon?.DiscountAmount ?? 0;
            return Products.Sum(p => p.Price) - discount;
        }
    }

    [ForeignKey(nameof(UserId))]
    public virtual DomainUser? User { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }

    [ForeignKey(nameof(CouponId))]
    public virtual Coupon? Coupon { get; set; }

    private readonly CartValidator _validator;
    public CartDetails()
    {
        _validator = new CartValidator();
    }
    private void ValidateAndThrow()
    {
        var validationResult = _validator.Validate(this);
        if (!validationResult.IsValid)
        {
            var exception = new BrandInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }
    public static CartDetails AddProductIntoCart(Guid UserId, Guid ProductId, int Quantity)
    {
        var objectToValidate = new CartDetails
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            ProductId = ProductId,
            Quantity = Quantity
        }; 
        objectToValidate.ValidateAndThrow();

        return objectToValidate;
    }
    public void UpdateCart(int quantity)
    {
        Quantity = quantity;
        ValidateAndThrow();
    }
    public void ApplyCoupon(Guid couponId)
    {
        CouponId = couponId;
        ValidateAndThrow();
    }
    public void DeleteProductInCart(Guid productId)
    {
        ProductId = productId;
        ValidateAndThrow();
    }
}
