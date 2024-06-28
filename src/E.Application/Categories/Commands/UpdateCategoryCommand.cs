using E.Application.Models;
using E.Domain.Entities;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.Commands;

public class UpdateCategoryCommand: BaseEntity, IRequest<OperationResult<Category>>
{
    public string CategoryName { get; set; }
}