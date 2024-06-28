using E.Application.Categories.Queries;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.QueryHandlers;

public class GetCategoriesQueryHandler
    : IRequestHandler<GetCategoriesQuery, OperationResult<IEnumerable<Category>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetCategoriesQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<IEnumerable<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<IEnumerable<Category>>();
        result.Payload = await _readUnitOfWork.Categories.GetAllAsync();
        return result;
    }
}