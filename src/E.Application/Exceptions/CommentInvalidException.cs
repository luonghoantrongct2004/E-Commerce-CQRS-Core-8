namespace E.Domain.Exceptions;

public class CommentInvalidException : DomainModelInvalidException
{
    internal CommentInvalidException() { }
    internal CommentInvalidException(string message) : base(message) { }
    internal CommentInvalidException(string message, Exception inner) : base(message, inner) { }
}