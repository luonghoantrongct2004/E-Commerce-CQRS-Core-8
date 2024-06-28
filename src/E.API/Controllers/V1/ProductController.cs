using AutoMapper;
using E.API.Contracts.Common;
using E.API.Contracts.Products.Requests;
using E.API.Contracts.Products.Responses;
using E.Application.Products.Commands;
using E.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : BaseController
{
    public ProductController(IMediator mediator, IMapper mapper,
        IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger)
        : base(mediator, mapper, errorResponseHandler, logger)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAllProducts());
        var mapped = _mapper.Map<IEnumerable<ProductResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductCreate newProduct, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand
        {
            ProductName = newProduct.ProductName,
            Description = newProduct.Description,
            Price = newProduct.Price,
            Images = newProduct.Images,
            CategoryId = newProduct.CategoryId ?? Guid.Empty,
            BrandId = newProduct.BrandId ?? Guid.Empty,
            StockQuantity = newProduct.StockQuantity,
            CreatedAt = newProduct.CreatedAt ?? DateTime.UtcNow,
            Discount = newProduct.Discount ?? 0,
        };
        var result = await _mediator.Send(command);
        var mapped = _mapper.Map<ProductResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors)
            : CreatedAtAction(nameof(GetAllProducts), new { id = mapped.ProductId }, mapped);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] ProductUpdate updatedProduct)
    {
        var command = new UpdateProductCommand
        {
            ProductId = id,
            ProductName = updatedProduct.ProductName,
            Description = updatedProduct.Description,
            Price = updatedProduct.Price,
            Images = updatedProduct.Images,
            CategoryId = updatedProduct.CategoryId,
            BrandId = updatedProduct.BrandId,
            StockQuantity = updatedProduct.StockQuantity,
            CreatedAt = updatedProduct.CreatedAt ?? DateTime.UtcNow,
            Discount = updatedProduct.Discount,
        };
        var result = await _mediator.Send(command);
        var mapped = _mapper.Map<ProductResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors)
            : Ok(mapped);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteProductCommand
        {
            ProductId = id
        };
        var result = await _mediator.Send(command);
        return result.IsError ? HandleErrorResponse(result.Errors)
            : NoContent();
    }
}
