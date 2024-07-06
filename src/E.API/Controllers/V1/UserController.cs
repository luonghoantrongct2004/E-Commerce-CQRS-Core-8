using E.API.Contracts.Users.Responses;

namespace E.API.Controllers.V1;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : BaseController
{
    public UserController(IMediator mediator, IMapper mapper,
        IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger)
        : base(mediator, mapper, errorResponseHandler, logger)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllUserQuery();
        var response = await _mediator.Send(query);
        var users = _mapper.Map<List<UserResponse>>(response.Payload);
        return Ok(users);
    }

    [Route(ApiRoutes.IdRoute)]
    [HttpGet]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetUserQuery { UserId = id };
        var response = await _mediator.Send(query);

        if (response.IsError) return HandleErrorResponse(response.Errors);

        var user = _mapper.Map<UserResponse>(response.Payload);
        return Ok(user);
    }

    [Route(ApiRoutes.IdRoute)]
    [HttpPut]
    public async Task<IActionResult> Put(Guid id, UpdateUserCommand updateUser)
    {
        var command = _mapper.Map<UpdateUserCommand>(updateUser);
        command.UserId = id;
        var response = await _mediator.Send(command);

        return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
    }

    [Route(ApiRoutes.IdRoute)]
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new RemoveUserCommand { IdentityUserId = id };
        var result = await _mediator.Send(command);

        if (result.IsError) return HandleErrorResponse(result.Errors);

        return NoContent();
    }
}