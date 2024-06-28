using AutoMapper;
using E.API.Agregrates;
using E.API.Contracts.Brands.Requests;
using E.API.Contracts.Brands.Responses;
using E.API.Contracts.Common;
using E.Application.Brands.Commands;
using E.Application.Brands.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1;

[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class BrandController : BaseController
{
    public BrandController(IMediator mediator, IMapper mapper,
        IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger)
        : base(mediator, mapper, errorResponseHandler, logger)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Gets()
    {
        var query = new GetBrandsQuery();
        var response = await _mediator.Send(query);
        var brands = _mapper.Map<List<BrandResponse>>(response.Payload);
        return Ok(brands);
    }

    [HttpGet(ApiRoutes.IdRoute)]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetBrandQuery { Id = id };
        var response = await _mediator.Send(query);
        var brand = _mapper.Map<BrandResponse>(response.Payload);
        return Ok(brand);
    }

    [HttpPost(ApiRoutes.Brand.Create)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Post([FromBody] BrandCreate newBrand)
    {
        var command = _mapper.Map<CreateBrandCommand>(newBrand);
        var response = await _mediator.Send(command);
        var mapped = _mapper.Map<BrandResponse>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors)
                : CreatedAtAction(nameof(Get), new { id = mapped.Id }, mapped);
    }

    [HttpPut(ApiRoutes.Brand.Update)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Put(Guid brandId, [FromBody] BrandUpdate brandUpdate)
    {
        var command = _mapper.Map<UpdateBrandCommand>(brandUpdate);
        command.Id = brandId;
        var response = await _mediator.Send(command);

        return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
    }

    [HttpDelete(ApiRoutes.IdRoute)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new RemoveBrandCommand { BrandId = id };
        var response = await _mediator.Send(command);

        if (response.IsError) return HandleErrorResponse(response.Errors);

        return NoContent();
    }
}
