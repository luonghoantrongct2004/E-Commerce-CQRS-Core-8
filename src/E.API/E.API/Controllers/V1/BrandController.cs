using AutoMapper;
using E.API.Contracts.Brands.Requests;
using E.API.Contracts.Brands.Responses;
using E.API.Contracts.Common;
using E.Application.Brands.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1;

public class BrandController : BaseController
{
    public BrandController(IMediator mediator, IMapper mapper, IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger) : base(mediator, mapper, errorResponseHandler, logger)
    {
    }
    [HttpGet]
    public IActionResult GetAllBrand()
    {
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> CreateBrand([FromBody] BrandCreate newBrand)
    {
        var command = new CreateBrandCommand
        {
            BrandName = newBrand.BrandName
        };
        var result = await _mediator.Send(command);
        var mapped = _mapper.Map<BrandResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors)
                : CreatedAtAction(nameof(GetAllBrand), mapped);
    }
}
