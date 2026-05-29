using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class EmailValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyValueException(nameof(Email));
        if (!value.Contains('@') || !value.Contains('.'))
            throw new InvalidEmailFormatException(value);
    }
}