using E.API.Contracts.Coupons.Requests;
using E.API.Contracts.Coupons.Responses;

namespace E.API.Controllers.V1;
public class CouponController : BaseController
{
    public CouponController(IMediator mediator, IMapper mapper,
        IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger)
        : base(mediator, mapper, errorResponseHandler, logger)
    {
    }
    [HttpGet]
    public async Task<IActionResult> Gets()
    {
        var query = new GetCouponsQuery();
        var response = await _mediator.Send(query);
        var coupons = _mapper.Map<List<CouponResponse>>(response.Payload);
        return Ok(coupons);
    }
    [HttpGet(ApiRoutes.IdRoute)]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetCouponQuery { Id = id};
        var response = await _mediator.Send(query);
        var order = _mapper.Map<CouponResponse>(response.Payload);
        return Ok(order);
    }
    [HttpPost(ApiRoutes.Coupon.ApplyCoupon)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Post([FromBody] ApplyCoupon applyCoupon)
    {
        var command = _mapper.Map<ApplyCouponCommand>(applyCoupon);
        var response = await _mediator.Send(command);
        var mapped = _mapper.Map<CouponResponse>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors)
                : CreatedAtAction(nameof(Get), new { id = mapped.Id }, mapped);
    }
    [HttpPost(ApiRoutes.Coupon.CreateCoupon)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Post([FromBody] CreateCoupon newCoupon)
    {
        var command = _mapper.Map<CreateCouponCommand>(newCoupon);
        var response = await _mediator.Send(command);
        var mapped = _mapper.Map<CouponResponse>(response.Payload);
        return response.IsError ? HandleErrorResponse(response.Errors)
                : CreatedAtAction(nameof(Get), new { id = mapped.Id }, mapped);
    }

    [HttpPut(ApiRoutes.Coupon.UpdateCoupon)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Put(Guid couponId)
    {
        var command = new UpdateCouponCommand { Id = couponId};
        var response = await _mediator.Send(command);

        if (response.IsError) return HandleErrorResponse(response.Errors);

        return NoContent();
    }

    [HttpDelete(ApiRoutes.Coupon.DisableCoupon)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid couponId)
    {
        var command = new DisableCouponCommand { Id = couponId };
        var response = await _mediator.Send(command);

        if (response.IsError) return HandleErrorResponse(response.Errors);

        return NoContent();
    }
}
