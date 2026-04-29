using Hospital.Domain.ValueObjects.Base;
using HospitalSystem.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class SpecializationValidator : IValidator<string>
{
    public static int MIN_LENGTH => 3;
    public static int MAX_LENGTH => 50;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length < MIN_LENGTH)
            throw new SpecializationShortValueException(value, MIN_LENGTH);
        if (value.Length > MAX_LENGTH)
            throw new SpecializationLongValueException(value, MAX_LENGTH);
    }
}