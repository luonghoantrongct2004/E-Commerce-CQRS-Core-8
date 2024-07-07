using E.Application.Models;
using MediatR;

namespace E.Application.Categories.Commands;

public class DisableCategoryCommand : IRequest<OperationResult<bool>>
{
    public Guid CategoryId { get; set; }
}