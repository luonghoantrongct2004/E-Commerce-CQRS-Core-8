namespace E.Domain.Exceptions;

public class BrandInvalidException:DomainModelInvalidException
{
    internal BrandInvalidException() { }
    internal BrandInvalidException(string message) : base(message) { }
    internal BrandInvalidException(string message, Exception inner) : base(message, inner) { }
}