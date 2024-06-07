using AutoMapper;
using E.API.Contracts.Common;
using E.API.Contracts.Products.Responses;
using E.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1;

public class ProductController : BaseController
{
    public ProductController(IMediator mediator, IMapper mapper, IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger) : base(mediator, mapper, errorResponseHandler, logger)
    {
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProduct()
    {
        var result = await _mediator.Send(new GetAllProducts());
        var mapper = _mapper.Map<IEnumerable<ProductResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapper);
    }
}