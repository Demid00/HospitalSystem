using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class NullableNotEmptyValidator : IValidator<string?>
{
    public void Validate(string? value)
    {
        if (value != null && string.IsNullOrWhiteSpace(value))
            throw new EmptyValueException("Value");
    }
}