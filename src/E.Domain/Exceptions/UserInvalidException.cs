namespace E.Domain.Exceptions;

public class UserInvalidException : DomainModelInvalidException
{
    internal UserInvalidException() { }
    internal UserInvalidException(string message) : base(message) { }
    internal UserInvalidException(string message, Exception inner) : base(message, inner) { }
}