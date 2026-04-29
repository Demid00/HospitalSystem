namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class MoneyAmountNonPositiveException : ArgumentException
{
    public decimal Value { get; }

    public MoneyAmountNonPositiveException(decimal value)
        : base($"Money amount {value} must be positive.", "amount")
    {
        Value = value;
    }

    public MoneyAmountNonPositiveException(decimal value, string paramName)
        : base($"Money amount {value} must be positive.", paramName)
    {
        Value = value;
    }
}