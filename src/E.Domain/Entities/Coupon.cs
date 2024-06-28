using E.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace E.Domain.Models
{
    public class Coupon : BaseEntity
    {
        [Required]
        [Display(Name ="Mã giảm giá")]
        public string CouponCode { get; set; }
        [Required]
        [Display(Name = "Số tiền giảm giá")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "Số tiền giảm tối thiểu")]
        public int MinAmount { get; set; }
    }
}
