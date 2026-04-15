namespace Hospital.ValueObjects.Exceptions;

public class ArgumentLongValueException : FormatException
{
    public string Value { get; }
    public int MaxLength { get; }

    public ArgumentLongValueException(string paramName, string value, int maxLength)
        : base($"The \"{paramName}\" length {value.Length} is greater than maximum allowed length {maxLength}")
    {
        Value = value;
        MaxLength = maxLength;
    }
}