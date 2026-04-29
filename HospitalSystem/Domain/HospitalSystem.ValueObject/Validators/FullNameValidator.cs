using Hospital.Domain.ValueObjects.Base;
using HospitalSystem.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class FullNameValidator : IValidator<string>
{
    public static int MIN_LENGTH => 2;
    public static int MAX_LENGTH => 100;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length < MIN_LENGTH)
            throw new FullNameShortValueException(value, MIN_LENGTH);
        if (value.Length > MAX_LENGTH)
            throw new FullNameLongValueException(value, MAX_LENGTH);
    }
}