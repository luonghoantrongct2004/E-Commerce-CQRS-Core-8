using System.ComponentModel.DataAnnotations;

namespace E.API.Contracts.Carts.Responses;

public class CartResponse : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();

    public Guid CouponId { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public decimal CartTotal { get; set; }
}