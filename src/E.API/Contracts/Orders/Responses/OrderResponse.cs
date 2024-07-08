using E.Domain.Entities.Orders;

namespace E.API.Contracts.Orders.Responses;

public class OrderResponse
{
    public Guid UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? Note { get; set; }
    public string PaymentMethod { get; set; }
    public string ContactPhone { get; set; }
    public decimal TotalPrice { get; set; }

    public ICollection<Orderdetail> OrderDetails { get; set; }
}