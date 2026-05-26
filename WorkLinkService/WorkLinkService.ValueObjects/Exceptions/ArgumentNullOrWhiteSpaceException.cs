namespace WorkLinkService.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException(string paramName)
    : ArgumentException($"The \"{paramName}\" cannot be null, empty or whitespace.", paramName);