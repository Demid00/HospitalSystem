namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class FullNameLongValueException : ArgumentException
{
    public string Value { get; }
    public int MaxLength { get; }

    public FullNameLongValueException(string value, int maxLength)
        : base($"Full name '{value}' length {value.Length} is greater than maximum length {maxLength}.", "value")
    {
        Value = value;
        MaxLength = maxLength;
    }
}