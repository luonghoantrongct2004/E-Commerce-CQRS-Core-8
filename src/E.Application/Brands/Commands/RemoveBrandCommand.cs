using E.Application.Models;
using MediatR;

namespace E.Application.Brands.Commands;

public class RemoveBrandCommand : IRequest<OperationResult<bool>>
{
    public Guid BrandId { get; set; }
}