using WorkLink.Domain.Enums;

namespace WorkLink.Domain.ValueObjects;

public class ContactInfo
{
    public string Value { get; }
    public ContactType Type { get; }

    public ContactInfo(string value, ContactType type)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Контакт не может быть пустым");

        Value = value;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Type}: {Value}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ContactInfo other)
            return false;
        return Value == other.Value && Type == other.Type;
    }

    public override int GetHashCode()
    {
        return (Value, Type).GetHashCode();
    }
}