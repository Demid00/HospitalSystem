namespace Hospital.ValueObjects.Exceptions;

public class ArgumentMinValueException : ArgumentException
{
    public object Value { get; }
    public object MinValue { get; }

    public ArgumentMinValueException(string paramName, object value, object minValue)
        : base($"Value {value} is less than minimum allowed value {minValue}.", paramName)
    {
        Value = value;
        MinValue = minValue;
    }
}