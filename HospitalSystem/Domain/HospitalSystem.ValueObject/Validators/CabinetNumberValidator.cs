using Hospital.Domain.ValueObjects.Base;
using HospitalSystem.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class CabinetNumberValidator : IValidator<int>
{
    public static int MIN_VALUE => 1;
    public static int MAX_VALUE => 999;

    public void Validate(int value)
    {
        if (value < MIN_VALUE)
            throw new CabinetNumberMinValueException(value, MIN_VALUE);
        if (value > MAX_VALUE)
            throw new CabinetNumberMaxValueException(value, MAX_VALUE);
    }
}