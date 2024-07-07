using E.Application.Models;
using MediatR;

namespace E.Application.Brands.Commands;

public class DisableBrandCommand : IRequest<OperationResult<bool>>
{
    public Guid BrandId { get; set; }
}