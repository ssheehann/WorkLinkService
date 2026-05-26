namespace WorkLinkService.Domain.Exceptions;

public class ArgumentNullValueException(string paramName)
    : ArgumentNullException(paramName, $"The \"{paramName}\" cannot be null.");