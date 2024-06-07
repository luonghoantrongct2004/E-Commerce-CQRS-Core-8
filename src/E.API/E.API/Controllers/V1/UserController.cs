using AutoMapper;
using E.Application.Users.Queries;
using E.Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E.API.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetAllUsers();
            var response = _mediator.Send(query);
            var user = _mapper.Map<List<BasicUser>>(response);
            return Ok(user);
        }
    }
}
