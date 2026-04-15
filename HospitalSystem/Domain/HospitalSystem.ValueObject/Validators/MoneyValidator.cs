// Validators/MoneyValidator.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Exceptions;

namespace Hospital.ValueObjects.Validators;

public class MoneyValidator : IValidator<decimal>
{
    public static decimal MIN_AMOUNT => 0;
    public static decimal MAX_AMOUNT => 1_000_000;

    public void Validate(decimal value)
    {
        if (value < MIN_AMOUNT)
            throw new ArgumentMinValueException(nameof(value), value, MIN_AMOUNT);
        if (value > MAX_AMOUNT)
            throw new ArgumentMaxValueException(nameof(value), value, MAX_AMOUNT);
        if (decimal.Round(value, 2) != value)
            throw new InvalidMoneyPrecisionException(value);
    }
}