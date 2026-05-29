namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class SpecializationShortValueException : ArgumentException
{
    public string Value { get; }
    public int MinLength { get; }

    public SpecializationShortValueException(string value, int minLength)
        : base($"Specialization '{value}' length {value.Length} is less than minimum length {minLength}.", "value")
    {
        Value = value;
        MinLength = minLength;
    }
}