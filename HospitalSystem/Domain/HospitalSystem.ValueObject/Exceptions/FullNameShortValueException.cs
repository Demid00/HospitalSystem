namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class FullNameShortValueException : ArgumentException
{
    public string Value { get; }
    public int MinLength { get; }

    public FullNameShortValueException(string value, int minLength)
        : base($"Full name '{value}' length {value.Length} is less than minimum length {minLength}.", "value")
    {
        Value = value;
        MinLength = minLength;
    }
}