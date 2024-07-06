using E.Domain.Entities.Coupons;
using E.Domain.Entities.Products;
using E.Domain.Entities.Users;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
}