using E.Application.Identites.Commands;
using E.Application.Models;
using E.Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace E.Application.Identites.CommandHandlers;

public class LogoutCommandHandler 
    : IRequestHandler<LogoutCommand, OperationResult<bool>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SignInManager<DomainUser> _signInManager;

    public LogoutCommandHandler(IHttpContextAccessor httpContextAccessor,
        SignInManager<DomainUser> signInManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _signInManager = signInManager;
    }

    public async Task<OperationResult<bool>> Handle(LogoutCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            await _signInManager.SignOutAsync();

            _httpContextAccessor.HttpContext.Response.Cookies
                .Delete(".hoantrong");
        }catch (Exception ex)
        {
            result.AddError(Enums.ErrorCode.UnknownError, ex.Message);
        }
        return result;
    }
}