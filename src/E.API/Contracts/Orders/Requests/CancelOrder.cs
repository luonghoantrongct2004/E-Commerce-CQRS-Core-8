namespace E.API.Contracts.Orders.Requests;

public class CancelOrder
{
    public Guid Id { get; set; }
    public Product Product { get; set; }
}
