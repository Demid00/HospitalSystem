namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class CabinetNumberMaxValueException : ArgumentException
{
    public int Value { get; }
    public int MaxValue { get; }

    public CabinetNumberMaxValueException(int value, int maxValue)
        : base($"Cabinet number {value} is greater than maximum value {maxValue}.", "value")
    {
        Value = value;
        MaxValue = maxValue;
    }
}