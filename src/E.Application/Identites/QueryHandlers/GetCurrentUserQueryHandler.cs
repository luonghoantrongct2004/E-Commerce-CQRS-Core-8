using AutoMapper;
using E.Application.Identity.Queries;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E.Application.Identity.QueryHandlers;

public class GetCurrentUserQueryHandler :
    IRequestHandler<GetCurrentUserQuery, OperationResult<IdentityUserDto>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly UserManager<DomainUser> _userManager;
    private readonly IMapper _mapper;
    private OperationResult<IdentityUserDto> _result = new();

    public GetCurrentUserQueryHandler(IReadUnitOfWork readUnitOfWork, UserManager<DomainUser> userManager, IMapper mapper)
    {
        _readUnitOfWork = readUnitOfWork;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var identityIdClaim = request.ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == "IdentityId")?.Value;
        if (identityIdClaim == null || !Guid.TryParse(identityIdClaim, out Guid userIdGuid))
        {
            return _result;
        }

        var identity = await _userManager.FindByIdAsync(identityIdClaim);
        var user = await _readUnitOfWork.Users.FirstOrDefaultAsync(u => u.Id == request.UserProfileId);

        _result.Payload = _mapper.Map<IdentityUserDto>(user);
        _result.Payload.UserName = identity.Email;
        return _result;
    }
}