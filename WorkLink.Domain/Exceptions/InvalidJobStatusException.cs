namespace WorkLink.Domain.Exceptions;

public class InvalidJobStatusException : DomainException
{
    public InvalidJobStatusException(string message) : base(message) { }
}