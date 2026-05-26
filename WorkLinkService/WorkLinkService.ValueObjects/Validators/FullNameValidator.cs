using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Exceptions;

namespace WorkLinkService.ValueObjects.Validators;

public class FullNameValidator : IValidator<string>
{
    public static int MAX_LENGTH => 300;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}