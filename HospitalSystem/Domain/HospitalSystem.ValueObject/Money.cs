using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Validators;

namespace Hospital.Domain.ValueObjects;

public class Money(decimal amountInRub) : ValueObject<decimal>(
    new MoneyValidator(),
    Math.Round(amountInRub, 2, MidpointRounding.AwayFromZero))
{
    public static Money operator +(Money m1, Money m2) => new(m1.Value + m2.Value);
    public static Money operator -(Money m1, Money m2) => new(m1.Value - m2.Value);
    public static bool operator >(Money m1, Money m2) => m1.Value > m2.Value;
    public static bool operator <(Money m1, Money m2) => m1.Value < m2.Value;
    public static bool operator >=(Money m1, Money m2) => m1.Value >= m2.Value;
    public static bool operator <=(Money m1, Money m2) => m1.Value <= m2.Value;

    public override string ToString() => $"{Value:F2} RUB";
}