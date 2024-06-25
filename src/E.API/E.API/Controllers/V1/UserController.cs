using AutoMapper;
using E.API.Agregrates;
using E.API.Contracts.Common;
using E.API.Contracts.Users.Responses;
using E.Application.Identity.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var query =  new GetAllUserQuery();
        var response = await _mediator.Send(query);
        var users = _mapper.Map<List<UserResponse>>(response.Payload);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}