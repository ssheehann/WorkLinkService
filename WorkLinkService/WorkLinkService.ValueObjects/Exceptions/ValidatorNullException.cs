namespace WorkLinkService.ValueObjects.Exceptions;

public class ValidatorNullException(string paramName)
    : ArgumentNullException(paramName, $"Validator for \"{paramName}\" cannot be null.");