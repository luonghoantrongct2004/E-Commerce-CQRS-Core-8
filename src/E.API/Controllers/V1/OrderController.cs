using E.API.Contracts.Orders.Requests;
using E.API.Contracts.Orders.Responses;

namespace E.API.Controllers.V1;
public class OrderController : BaseController
{
    public OrderController(IMediator mediator, IMapper mapper,
        IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger) 
        : base(mediator, mapper, errorResponseHandler, logger)
    {
    }
    [HttpGet]
    public async Task<IActionResult> Gets()
    {
        var query = new GetOrdersQuery();
        var response = await _mediator.Send(query);
        var orders = _mapper.Map<List<OrderResponse>>(response.Payload);
        return Ok(orders);
    }

    [HttpGet(ApiRoutes.IdRoute)]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetOrderQuery { Id = id };
        var response = await _mediator.Send(query);
        var order = _mapper.Map<OrderResponse>(response.Payload);
        return Ok(order);
    }

    [HttpPost(ApiRoutes.Order.Add)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Post([FromBody] AddOrder newOrder)
    {
        var command = _mapper.Map<AddOrderCommand>(newOrder);
        var response = await _mediator.Send(command);
        var mapped = _mapper.Map<OrderResponse>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors)
                : CreatedAtAction(nameof(Get), new { id = mapped.UserId }, mapped);
    }

    [HttpPut(ApiRoutes.Order.ConfirmOrder)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ConfirmOrder(Guid orderId, Guid productId)
    {
        var command = new ConfirmOrderCommand { Id = orderId, ProductId = productId };
        var response = await _mediator.Send(command);

        if (response.IsError) return HandleErrorResponse(response.Errors);

        return NoContent();
    }

    [HttpDelete(ApiRoutes.Order.CancelOrder)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CancelOrder(Guid orderId, Guid productId)
    {
        var command = new CancelOrderCommand { Id = orderId, ProductId = productId };
        var response = await _mediator.Send(command);

        if (response.IsError) return HandleErrorResponse(response.Errors);

        return NoContent();
    }
}
