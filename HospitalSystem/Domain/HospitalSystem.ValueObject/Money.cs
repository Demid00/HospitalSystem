// Money.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Validators;
using Hospital.ValueObjects.Exceptions;

namespace Hospital.ValueObjects;

public class Money : ValueObject<decimal>
{
    public string Currency { get; } = "RUB";

    public Money(decimal amount) : base(new MoneyValidator(), amount) { }

    public Money Add(Money other)
    {
        return new Money(Value + other.Value);
    }

    public Money Subtract(Money other)
    {
        if (other.Value > Value)
            throw new InsufficientFundsException(other.Value, Value);
        return new Money(Value - other.Value);
    }

    public Money Multiply(int multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("Multiplier cannot be negative", nameof(multiplier));
        return new Money(Value * multiplier);
    }
}