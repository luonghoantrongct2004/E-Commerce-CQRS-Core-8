using E.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace E.Domain.Entities.Orders;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? ShipDate { get; set; }

    public DateTime PaymentDate { get; set; }

    public Guid PaymentId { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ShipperId { get; set; }
    public DomainUser User { get; set; }
    public string Status { get; set; }
    public string PaymentMethod { get; set; }

    public string ShippingAddress { get; set; }
    public string ContactPhone { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public decimal TotalPrice { get; set; }

    public ICollection<Orderdetail> OrderDetails { get; set; }

    /*public virtual Shipper Shipper { get; set; }*/
}