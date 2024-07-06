namespace E.Domain.Exceptions;

public class CategoryInvalidException:DomainModelInvalidException
{
    internal CategoryInvalidException() { }
    internal CategoryInvalidException(string message) : base(message) { }
    internal CategoryInvalidException(string message, Exception inner) : base(message, inner) { }
}