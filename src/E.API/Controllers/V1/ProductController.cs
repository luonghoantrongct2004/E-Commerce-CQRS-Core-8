using AutoMapper;
using E.API.Agregrates;
using E.API.Contracts.Common;
using E.API.Contracts.Products.Requests;
using E.API.Contracts.Products.Responses;
using E.Application.Products.Commands;
using E.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1;

[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class ProductController : BaseController
{
    public ProductController(IMediator mediator, IMapper mapper,
        IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger)
        : base(mediator, mapper, errorResponseHandler, logger)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Gets()
    {
        var response = await _mediator.Send(new GetAllProducts());
        var mapped = _mapper.Map<IEnumerable<ProductResponse>>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors) : Ok(mapped);
    }

    [HttpGet(ApiRoutes.IdRoute)]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllProducts());
        var mapped = _mapper.Map<IEnumerable<ProductResponse>>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors) : Ok(mapped);
    }

    [HttpPost(ApiRoutes.Product.Create)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Post([FromBody] ProductCreate newProduct,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateProductCommand>(newProduct);
        var response = await _mediator.Send(command);
        var mapped = _mapper.Map<ProductResponse>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors)
            : CreatedAtAction(nameof(GetAllProducts), new { id = mapped.Id }, mapped);
    }

    [HttpPut(ApiRoutes.Product.Update)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Put(Guid id, [FromBody] ProductUpdate updatedProduct)
    {
        var command = _mapper.Map<UpdateProductCommand>(updatedProduct);
        command.Id = id;
        var response = await _mediator.Send(command);
        var mapped = _mapper.Map<ProductResponse>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors)
            : Ok(mapped);
    }

    [HttpDelete(ApiRoutes.IdRoute)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new RemoveProductCommand { Id = id };
        var response = await _mediator.Send(command);
        return response.IsError ? HandleErrorResponse(response.Errors)
            : NoContent();
    }
}