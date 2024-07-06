namespace E.Domain.Exceptions;

public class CouponInvalidException : DomainModelInvalidException
{
    internal CouponInvalidException() { }
    internal CouponInvalidException(string message) : base(message) { }
    internal CouponInvalidException(string message, Exception inner) : base(message, inner) { }
}