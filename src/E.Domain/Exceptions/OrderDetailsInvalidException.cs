namespace E.Domain.Exceptions;

public class OrderDetailsInvalidException : DomainModelInvalidException
{
    internal OrderDetailsInvalidException() { }
    internal OrderDetailsInvalidException(string message) : base(message) { }
    internal OrderDetailsInvalidException(string message, Exception inner) : base(message, inner) { }
}