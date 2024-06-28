using E.Domain.Entities;
using E.Domain.Entities.Products;

namespace E.Domain.Models
{
    public class Orderdetail : BaseEntity
    {

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime? ShipDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
