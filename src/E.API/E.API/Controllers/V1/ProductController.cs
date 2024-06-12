using AutoMapper;
using E.API.Contracts.Common;
using E.API.Contracts.Products.Requests;
using E.API.Contracts.Products.Responses;
using E.Application.Products.Commands;
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
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreate newProduct, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand
        {
            ProductName = newProduct.ProductName,
            Description = newProduct.Description,
            Price = newProduct.Price,
            Images = newProduct.Images,
            CategoryId = (Guid)newProduct.CategoryId,
            BrandId = (Guid)newProduct.BrandId,
            StockQuantity = newProduct.StockQuantity,
            CreatedAt = newProduct.CreatedAt,
            Discount = (int)newProduct.Discount,
        };
        var result = await _mediator.Send(command);
        var mapped = _mapper.Map<ProductResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors)
                : CreatedAtAction(nameof(GetAllProduct), mapped);
    }
}