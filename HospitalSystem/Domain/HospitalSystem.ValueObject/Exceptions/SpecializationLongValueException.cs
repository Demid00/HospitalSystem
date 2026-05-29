namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class SpecializationLongValueException : ArgumentException
{
    public string Value { get; }
    public int MaxLength { get; }

    public SpecializationLongValueException(string value, int maxLength)
        : base($"Specialization '{value}' length {value.Length} is greater than maximum length {maxLength}.", "value")
    {
        Value = value;
        MaxLength = maxLength;
    }
}