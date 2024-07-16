using E.API.Contracts.Identities;
using E.API.Extension;
using E.Application.Identites.Commands;
using E.Application.Token.Commands;

namespace E.API.Controllers.V1;

public class IdentityController : BaseController
{
    public IdentityController(IMediator mediator, IMapper mapper,
        IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger) :
        base(mediator, mapper, errorResponseHandler, logger)
    {
    }

    [HttpGet]
    [Route(ApiRoutes.Identity.CurrentUser)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetCurentUser()
    {
        var userId = HttpContext.GetIdentityIdClaimValue();

        var query = new GetCurrentUserQuery { UserProfileId = userId, ClaimsPrincipal = HttpContext.User };
        var result = await _mediator.Send(query);

        if (result.IsError) return HandleErrorResponse(result.Errors);

        return Ok(_mapper.Map<IdentityUserDto>(result.Payload));
    }

    [HttpPost]
    [Route(ApiRoutes.Identity.Login)]
    public async Task<IActionResult> Login(Login login)
    {
        var command = _mapper.Map<LoginCommand>(login);
        var result = await _mediator.Send(command);
        if (result.IsError) return HandleErrorResponse(result.Errors);
        return Ok(_mapper.Map<IdentityUserDto>(result.Payload));
    }

    [HttpPost]
    [Route(ApiRoutes.Identity.Logout)]
    public async Task<IActionResult> Logout()
    {
        var result = await _mediator.Send(new LogoutCommand());
        if (result.IsError) return HandleErrorResponse(result.Errors);
        return NoContent();
    }

    [HttpPost]
    [Route(ApiRoutes.Identity.Register)]
    public async Task<IActionResult> Register(UserRegister registration)
    {
        var command = _mapper.Map<RegisterUserCommand>(registration);
        var result = await _mediator.Send(command);

        if (result.IsError) return HandleErrorResponse(result.Errors);
        return Ok(_mapper.Map<IdentityUser>(result.Payload));
    }

    [HttpPost]
    [Route(ApiRoutes.Identity.RefreshToken)]
    public async Task<IActionResult> RefreshToken([FromBody] TokenCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Payload);
    }
}