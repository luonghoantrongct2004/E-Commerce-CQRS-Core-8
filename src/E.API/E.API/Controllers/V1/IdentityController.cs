using AutoMapper;
using E.API.Agregrates;
using E.API.Contracts.Common;
using E.API.Contracts.Identities;
using E.API.Extension;
using E.Application.Identity.Commands;
using E.Application.Identity.Queries;
using E.Domain.Entities.Users.Dto;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
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
    [Route(ApiRoutes.Identity.Register)]
    public async Task<IActionResult> Register(UserRegister registration)
    {
        var command = _mapper.Map<RegisterUserCommand>(registration);
        var result = await _mediator.Send(command);

        if (result.IsError) return HandleErrorResponse(result.Errors);
        return Ok(_mapper.Map<IdentityUser>(result.Payload));
    }
}