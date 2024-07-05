using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Orders.Commands;

public class ConfirmPurchar : IRequest
{
    public Product Product { get; set; }
}