namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class MoneyAmountHasMoreThanTwoDecimalPlacesException : ArgumentException
{
    public decimal Value { get; }

    public MoneyAmountHasMoreThanTwoDecimalPlacesException(decimal value)
        : base($"Money amount {value} has more than two decimal places.", "amount")
    {
        Value = value;
    }

    public MoneyAmountHasMoreThanTwoDecimalPlacesException(decimal value, string paramName)
        : base($"Money amount {value} has more than two decimal places.", paramName)
    {
        Value = value;
    }
}