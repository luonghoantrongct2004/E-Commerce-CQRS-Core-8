namespace E.Domain.Exceptions;

public class BrandInvailidException:DomainModelInvalidException
{
    internal BrandInvailidException() { }
    internal BrandInvailidException(string message) : base(message) { }
    internal BrandInvailidException(string message, Exception inner) : base(message, inner) { }
}