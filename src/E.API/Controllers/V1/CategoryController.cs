using E.API.Contracts.Categories.Requests;
using E.API.Contracts.Categories.Responses;

namespace E.API.Controllers.V1
{
    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator, IMapper mapper, IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger) : base(mediator, mapper, errorResponseHandler, logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var query = new GetCategoriesQuery();
            var response = await _mediator.Send(query);
            var brands = _mapper.Map<List<CategoryResponse>>(response.Payload);
            return Ok(brands);
        }

        [HttpGet(ApiRoutes.IdRoute)]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetCategoryQuery { Id = id };
            var response = await _mediator.Send(query);
            var brand = _mapper.Map<CategoryResponse>(response.Payload);
            return Ok(brand);
        }

        [HttpPost(ApiRoutes.Category.Create)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] CategoryCreate newCategory)
        {
            var command = _mapper.Map<CreateCategoryCommand>(newCategory);
            var response = await _mediator.Send(command);
            var mapped = _mapper.Map<CategoryResponse>(response.Payload);
            return response.IsError ? HandleErrorResponse(response.Errors)
                    : CreatedAtAction(nameof(Get), new { id = mapped.Id }, mapped);
        }

        [HttpPut(ApiRoutes.Category.Update)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put(Guid categoryId, [FromBody] CategoryUpdate categoryUpdate)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(categoryUpdate);
            command.Id = categoryId;
            var response = await _mediator.Send(command);

            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }

        [HttpDelete(ApiRoutes.IdRoute)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new RemoveCategoryCommand { CategoryId = id };
            var response = await _mediator.Send(command);

            if (response.IsError) return HandleErrorResponse(response.Errors);

            return NoContent();
        }
    }
}