using E.Application.Models;
using E.Domain.Entities.Token.Dto;
using MediatR;

namespace E.Application.Token.Commands;

public class TokenCommand : IRequest<OperationResult<TokenDto>>
{
    public string UserName { get; set; }
    public string RefreshToken { get; set; }
}