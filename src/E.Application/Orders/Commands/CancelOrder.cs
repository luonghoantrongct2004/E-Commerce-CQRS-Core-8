using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Orders.Commands;

public class CancelOrder : IRequest
{
    public Product Product { get; set; }
}