namespace E.API.Contracts.Orders.Requests;

public class AddOrder
{
    public DateTime CreatedAt { get; set; }
    public string? Note { get; set; }
    public string PaymentMethod { get; set; }
    public string ContactPhone { get; set; }

    public decimal TotalPrice { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}