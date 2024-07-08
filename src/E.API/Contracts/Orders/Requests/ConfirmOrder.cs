namespace E.API.Contracts.Orders.Requests;

public class ConfirmOrder
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
}
