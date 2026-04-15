namespace Hospital.ValueObjects.Exceptions;

public class InvalidMoneyPrecisionException : FormatException
{
    public decimal Amount { get; }

    public InvalidMoneyPrecisionException(decimal amount)
        : base($"Money amount {amount} has more than 2 decimal places.") => Amount = amount;
}