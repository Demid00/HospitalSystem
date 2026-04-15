namespace Hospital.ValueObjects.Exceptions;

public class ArgumentMaxValueException : ArgumentException
{
    public object Value { get; }
    public object MaxValue { get; }

    public ArgumentMaxValueException(string paramName, object value, object maxValue)
        : base($"Value {value} is greater than maximum allowed value {maxValue}.", paramName)
    {
        Value = value;
        MaxValue = maxValue;
    }
}