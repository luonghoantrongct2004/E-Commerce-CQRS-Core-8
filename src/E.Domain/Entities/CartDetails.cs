using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E.Domain.Entities.Products;
using E.Domain.Entities.Users;

namespace E.Domain.Models
{
    public class CartDetails
    {
        [Key]
        public Guid CartDetailsId { get; set; }
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
        public DomainUser? User { get; set; }
    }
}
