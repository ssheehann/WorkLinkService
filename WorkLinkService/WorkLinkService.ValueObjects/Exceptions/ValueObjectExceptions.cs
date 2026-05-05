namespace WorkLinkService.ValueObjects.Exceptions;

public class ValidatorNullException(string paramName) 
    : ArgumentNullException(paramName, $"Валидатор для \"{paramName}\" не может быть null");
public class ArgumentNullOrWhiteSpaceException(string paramName) 
    : ArgumentException($"Параметр \"{paramName}\" не может быть null, пустым или состоять только из пробелов", paramName);
public class ArgumentLongValueException(string paramName, string value, int maxLength)
    : FormatException($"Длина параметра \"{paramName}\" ({value.Length}) превышает максимально допустимую длину ({maxLength})")
{
    public string Value => value;
    public int MaxLength => maxLength;
}
public class ArgumentShortValueException(string paramName, string value, int minLength)
    : FormatException($"Длина параметра \"{paramName}\" ({value.Length}) меньше минимально допустимой длины ({minLength})")
{
    public string Value => value;
    public int MinLength => minLength;
}