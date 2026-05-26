using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Exceptions;

namespace WorkLinkService.ValueObjects.Validators;

public class EmailValidator : IValidator<string>
{
    public static int MAX_LENGTH => 254;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
        if (!value.Contains('@') || value.Length < 3)
            throw new FormatException($"The email \"{value}\" has invalid format.");
    }
}