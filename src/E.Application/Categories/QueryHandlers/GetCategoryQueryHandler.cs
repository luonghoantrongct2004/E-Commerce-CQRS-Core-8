using E.Application.Categories.Queries;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Categories;
using MediatR;

namespace E.Application.Categories.QueryHandlers;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, OperationResult<Category>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetCategoryQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<Category>> Handle(GetCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var result =  new OperationResult<Category>();
        var category = await _readUnitOfWork.Categories.FirstOrDefaultAsync(
            b => b.Id == request.Id);
        if(category is null)
        {
            result.AddError(ErrorCode.NotFound,
                CategoryErrorMessage.CategoryNotFound);
            return result;
        }
        result.Payload = category;
        return result;
    }
}