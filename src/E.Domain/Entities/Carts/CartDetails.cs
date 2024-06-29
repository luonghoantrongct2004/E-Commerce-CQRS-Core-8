using E.Domain.Entities.Products;
using E.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E.Domain.Entities.Carts;

public class CartDetails : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    [NotMapped]
    public Product? Product { get; set; }

    public int? CouponId { get; set; }

    [NotMapped]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public decimal Discount { get; set; }

    [NotMapped]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public decimal CartTotal { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual DomainUser? User { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product? Products { get; set; }
}