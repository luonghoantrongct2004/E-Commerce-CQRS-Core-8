namespace E.Domain.Exceptions;

public class ProductInvailidException : DomainModelInvalidException
{
    internal ProductInvailidException() { }
    internal ProductInvailidException(string message) : base(message) { }
    internal ProductInvailidException(string message, Exception inner) : base(message, inner) { }
}