namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class CabinetNumberMinValueException : ArgumentException
{
    public int Value { get; }
    public int MinValue { get; }

    public CabinetNumberMinValueException(int value, int minValue)
        : base($"Cabinet number {value} is less than minimum value {minValue}.", "value")
    {
        Value = value;
        MinValue = minValue;
    }
}