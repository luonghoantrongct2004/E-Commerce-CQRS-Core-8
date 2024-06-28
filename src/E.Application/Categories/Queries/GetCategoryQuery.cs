using E.Application.Models;
using E.Domain.Entities;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.Queries;

public class GetCategoryQuery : BaseEntity, IRequest<OperationResult<Category>>
{
}