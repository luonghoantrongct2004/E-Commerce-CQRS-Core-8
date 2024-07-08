namespace E.API.Contracts.Coupons.Requests;

public class ApplyCoupon
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal CartTotal { get; set; }
}
