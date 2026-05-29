using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class CabinetNumberValidator : IValidator<int>
{
    public void Validate(int value)
    {
        if (value < 1 || value > 999)
            throw new InvalidCabinetNumberException(value);
    }
}