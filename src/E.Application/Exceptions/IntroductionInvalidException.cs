namespace E.Domain.Exceptions;

public class IntroductionInvalidException : DomainModelInvalidException
{
    internal IntroductionInvalidException() { }
    internal IntroductionInvalidException(string message) : base(message) { }
    internal IntroductionInvalidException(string message, Exception inner) : base(message, inner) { }
}