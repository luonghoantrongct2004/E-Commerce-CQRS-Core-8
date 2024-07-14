using E.Application.Models;
using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Identites.Commands;

public class LogoutCommand : IRequest<OperationResult<bool>>
{
}