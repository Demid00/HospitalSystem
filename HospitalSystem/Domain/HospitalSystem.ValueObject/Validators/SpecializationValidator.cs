using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class SpecializationValidator : IValidator<string>
{
    public const int MinLength = 3;
    public const int MaxLength = 50;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyValueException(nameof(Specialization));
        if (value.Length < MinLength)
            throw new TooShortValueException(nameof(Specialization), MinLength);
        if (value.Length > MaxLength)
            throw new TooLongValueException(nameof(Specialization), MaxLength);
    }
}