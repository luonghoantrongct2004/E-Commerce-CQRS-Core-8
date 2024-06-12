using E.Domain.Entities.Products;

namespace E.Domain.Models
{
    public class Orderdetail
    {
        public Guid OrderdetailId { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime? ShipDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
