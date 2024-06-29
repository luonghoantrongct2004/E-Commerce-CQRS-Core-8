using System.ComponentModel.DataAnnotations;

namespace E.Domain.Entities.Coupons;

public class Coupon : BaseEntity
{
    public string CouponCode { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public decimal DiscountAmount { get; set; }

    public int MinAmount { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int UsageLimit { get; set; }

    public bool IsActive { get; set; } = true;

    public Coupon()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }
    public bool IsExpired()
    {
        return DateTime.UtcNow > ExpirationDate;
    }
}