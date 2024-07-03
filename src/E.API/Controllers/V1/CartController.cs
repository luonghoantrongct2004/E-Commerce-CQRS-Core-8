using E.API.Contracts.Carts.Requests;
using E.API.Contracts.Carts.Responses;

namespace E.API.Controllers.V1;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class CartController : BaseController
{
    public CartController(IMediator mediator, IMapper mapper,
        IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger)
        : base(mediator, mapper, errorResponseHandler, logger)
    {
    }

    [HttpGet(ApiRoutes.Cart.Gets)]
    public async Task<IActionResult> Gets(Guid userId)
    {
        var query = new GetCartsQuery { UserId = userId };
        var response = await _mediator.Send(query);
        var carts = _mapper.Map<List<CartDetails>>(response.Payload);
        return Ok(carts);
    }

    [HttpPost(ApiRoutes.Cart.Add)]
    public async Task<IActionResult> Post(Guid userId, Guid productId, [FromBody] CartItemAdd cartItem)
    {
        var command = _mapper.Map<CartItemAddCommand>(cartItem);
        command.ProductId = productId;
        command.UserId = userId;
        var response = await _mediator.Send(command);
        var mapped = _mapper.Map<CartResponse>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors)
            : CreatedAtAction(nameof(Gets), new { userId = mapped.UserId }, mapped);
    }

    [HttpDelete(ApiRoutes.Cart.Remove)]
    public async Task<IActionResult> Delete(Guid cartId)
    {
        var command = new CartItemRemoveCommand { CartDetailsId = cartId };
        var response = await _mediator.Send(command);

        if (response.IsError) return HandleErrorResponse(response.Errors);
        return NoContent();
    }
}