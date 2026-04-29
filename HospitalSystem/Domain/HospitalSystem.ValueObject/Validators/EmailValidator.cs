using Hospital.Domain.ValueObjects.Base;
using HospitalSystem.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class EmailValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (!value.Contains('@') || !value.Contains('.'))
            throw new InvalidEmailFormatException(value);
    }
}