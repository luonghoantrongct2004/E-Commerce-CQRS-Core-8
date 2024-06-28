using E.Application.Models;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.Queries;

public class GetCategoriesQuery : IRequest<OperationResult<IEnumerable<Category>>>
{
}