namespace E.Domain.Exceptions;

public class CategoryInvailidException:DomainModelInvalidException
{
    internal CategoryInvailidException() { }
    internal CategoryInvailidException(string message) : base(message) { }
    internal CategoryInvailidException(string message, Exception inner) : base(message, inner) { }
}