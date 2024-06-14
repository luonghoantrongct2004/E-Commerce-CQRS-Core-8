namespace E.Domain.Exceptions;

public class ProductInvalidException : DomainModelInvalidException
{
    internal ProductInvalidException() { }
    internal ProductInvalidException(string message) : base(message) { }
    internal ProductInvalidException(string message, Exception inner) : base(message, inner) { }
}