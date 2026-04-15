namespace Hospital.ValueObjects.Exceptions;

public class ArgumentShortValueException : FormatException
{
    public string Value { get; }
    public int MinLength { get; }

    public ArgumentShortValueException(string paramName, string value, int minLength)
        : base($"The \"{paramName}\" length {value.Length} is less than minimum allowed length {minLength}")
    {
        Value = value;
        MinLength = minLength;
    }
}