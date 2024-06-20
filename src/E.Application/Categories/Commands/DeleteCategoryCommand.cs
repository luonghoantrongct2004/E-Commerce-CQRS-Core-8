using E.Application.Models;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.Commands;

public class DeleteCategoryCommand: IRequest<OperationResult<Category>>
{
    public Guid CategoryId { get; set; }
}