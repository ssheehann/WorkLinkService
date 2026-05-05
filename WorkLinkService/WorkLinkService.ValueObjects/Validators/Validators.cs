using WorkLinkService.ValueObjects.Base;
using WorkLinkService.ValueObjects.Exceptions;

namespace WorkLinkService.ValueObjects.Validators;

public class JobTitleValidator : IValidator<string>
{
    public static int MAX_LENGTH => 200;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH) throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}
public class JobDescriptionValidator : IValidator<string>
{
    public static int MAX_LENGTH => 5000;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH) throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}
public class ContactInfoValidator : IValidator<string>
{
    public static int MAX_LENGTH => 500;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH) throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}
public class CompanyNameValidator : IValidator<string>
{
    public static int MAX_LENGTH => 200;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH) throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}
public class FullNameValidator : IValidator<string>
{
    public static int MAX_LENGTH => 300;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH) throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}
public class HashtagNameValidator : IValidator<string>
{
    public static int MAX_LENGTH => 100;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH) throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}
public class EmailValidator : IValidator<string>
{
    public static int MAX_LENGTH => 254;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH) throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
        if (!value.Contains('@') || value.Length < 3) throw new FormatException($"Электронная почта \"{value}\" имеет неверный формат");
    }
}