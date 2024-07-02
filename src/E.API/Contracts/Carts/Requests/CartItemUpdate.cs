namespace E.API.Contracts.Carts.Requests;

public class CartItemUpdate
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
