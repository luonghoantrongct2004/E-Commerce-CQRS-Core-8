using AutoMapper;
using E.API.Agregrates;
using E.API.Contracts.Carts.Requests;
using E.API.Contracts.Carts.Responses;
using E.API.Contracts.Common;
using E.Application.Carts.Commands;
using E.Application.Carts.Queries;
using E.Domain.Entities.Carts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1;

[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class CartController : BaseController
{
    public CartController(IMediator mediator, IMapper mapper,
        IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger) 
        : base(mediator, mapper, errorResponseHandler, logger)
    {
    }

    [HttpGet(ApiRoutes.IdRoute)]
    public async Task<IActionResult> Gets(Guid userId)
    {
        var query = new GetCartsQuery { UserId = userId };
        var response = await _mediator.Send(query);
        var carts = _mapper.Map<List<CartDetails>>(response.Payload);
        return Ok(carts);
    }

    [HttpPost(ApiRoutes.Cart.Add)]
    public async Task<IActionResult> Post([FromBody] CartItemAdd cartItem)
    {
        var command = _mapper.Map<CartItemAddCommand>(cartItem);
        var response = await _mediator.Send(command);
        var mapped = _mapper.Map<CartResponse>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors)
            : CreatedAtAction(nameof(Gets), new { userId = mapped.UserId }, mapped);
    }

    [HttpPut(ApiRoutes.Cart.Update)]
    public async Task<IActionResult> Put(Guid cartId, [FromBody] CartItemUpdate cartItem)
    {
        var command = _mapper.Map<CartItemUpdateCommand>(cartItem);
        command.CartDetailsId = cartId;
        var response = await _mediator.Send(command);

        return response.IsError ? HandleErrorResponse(response.Errors)
            : NoContent();
    }

    [HttpDelete(ApiRoutes.Cart.Remove)]
    public async Task<IActionResult> Delete(Guid cartId)
    {
        var command = new CartItemRemoveCommand { CartDetailsId = cartId };
        var response = await _mediator.Send(command);

        if(response.IsError) return HandleErrorResponse(response.Errors);
        return NoContent();
    }
}
