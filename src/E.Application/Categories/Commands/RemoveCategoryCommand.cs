using E.Application.Models;
using MediatR;

namespace E.Application.Categories.Commands;

public class RemoveCategoryCommand: IRequest<OperationResult<bool>>
{
    public Guid CategoryId { get; set; }
}