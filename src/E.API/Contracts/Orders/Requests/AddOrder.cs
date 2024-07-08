using E.Domain.Entities.Orders;

namespace E.API.Contracts.Orders.Requests;

public class AddOrder
{ 
    public List<Orderdetail> OrderDetails { get; set; }
    public string? Note { get; set; }
    public string PaymentMethod { get; set; }
    public string ContactPhone { get; set; }

    public decimal TotalPrice { get; set; }
}
