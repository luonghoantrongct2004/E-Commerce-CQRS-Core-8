namespace E.Domain.Exceptions;

public class OrderInvalidException : DomainModelInvalidException
{
    internal OrderInvalidException() { }
    internal OrderInvalidException(string message) : base(message) { }
    internal OrderInvalidException(string message, Exception inner) : base(message, inner) { }
}