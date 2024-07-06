namespace E.Domain.Exceptions;

public class CartInvalidException : DomainModelInvalidException
{
    internal CartInvalidException() { }
    internal CartInvalidException(string message) : base(message) { }
    internal CartInvalidException(string message, Exception inner) : base(message, inner) { }
}