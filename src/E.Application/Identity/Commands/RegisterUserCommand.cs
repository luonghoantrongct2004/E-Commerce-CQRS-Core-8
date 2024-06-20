using E.Application.Models;
using E.Domain.Entities.Users.Dto;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E.Application.Identity.Commands;

public class RegisterUserCommand : IRequest<OperationResult<IdentityUserDto>>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }
    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? CurrentCity { get; set; }
}