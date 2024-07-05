using MediatR;

namespace E.Application.Orders.Commands;

public class AddOrder : IRequest
{
    public Guid UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public string? Note { get; set; }
    public string PaymentMethod { get; set; }
    public string ContactPhone { get; set; }

    public decimal TotalPrice { get; set; }
}