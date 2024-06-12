using E.Application.Models;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.Commands;

public class CreateCategoryCommand:IRequest<OperationResult<Category>>
{
    public string CategoryName { get; set; }
}