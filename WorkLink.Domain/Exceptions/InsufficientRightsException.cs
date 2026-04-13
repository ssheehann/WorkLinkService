namespace WorkLink.Domain.Exceptions;

public class InsufficientRightsException : DomainException
{
    public InsufficientRightsException(string message) : base(message) { }
}