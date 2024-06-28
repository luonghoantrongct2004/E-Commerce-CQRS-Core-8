using AutoMapper;
using E.API.Contracts.Categories.Requests;
using E.API.Contracts.Categories.Responses;
using E.API.Contracts.Common;
using E.Application.Categories.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1
{
    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator, IMapper mapper, IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger) : base(mediator, mapper, errorResponseHandler, logger)
        {
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreate newCategory)
        {
            var command = new CreateCategoryCommand
            {
                CategoryName = newCategory.CategoryName
            };
            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<CategoryResponse>(result.Payload);
            return result.IsError ? HandleErrorResponse(result.Errors)
                : CreatedAtAction(nameof(GetAllCategory), mapped);
        }
    }
}
