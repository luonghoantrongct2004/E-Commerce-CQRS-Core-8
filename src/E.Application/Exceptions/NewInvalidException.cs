namespace E.Domain.Exceptions;

public class NewInvalidException : DomainModelInvalidException
{
    internal NewInvalidException() { }
    internal NewInvalidException(string message) : base(message) { }
    internal NewInvalidException(string message, Exception inner) : base(message, inner) { }
}