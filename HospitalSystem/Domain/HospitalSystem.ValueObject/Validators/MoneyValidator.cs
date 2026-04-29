using Hospital.Domain.ValueObjects.Base;
using HospitalSystem.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class MoneyValidator : IValidator<decimal>
{
    public void Validate(decimal value)
    {
        if (value <= 0)
            throw new MoneyAmountNonPositiveException(value);
        if (!IsValidAmount(value))
            throw new MoneyAmountHasMoreThanTwoDecimalPlacesException(value);
    }

    private static bool IsValidAmount(decimal value)
    {
        value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
        var remainder = value * 100 - (int)(value * 100);
        return remainder == 0m;
    }
}