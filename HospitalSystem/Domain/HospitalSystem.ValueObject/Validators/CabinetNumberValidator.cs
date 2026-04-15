// Validators/CabinetNumberValidator.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Exceptions;

namespace Hospital.ValueObjects.Validators;

public class CabinetNumberValidator : IValidator<int>
{
    public static int MIN_VALUE => 1;
    public static int MAX_VALUE => 999;

    public void Validate(int value)
    {
        if (value < MIN_VALUE)
            throw new ArgumentMinValueException(nameof(value), value, MIN_VALUE);
        if (value > MAX_VALUE)
            throw new ArgumentMaxValueException(nameof(value), value, MAX_VALUE);
    }
}