namespace E.API.Contracts.Carts.Requests;

public class CartItemAdd
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
