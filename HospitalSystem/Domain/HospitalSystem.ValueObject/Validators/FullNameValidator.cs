using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class FullNameValidator : IValidator<string>
{
    public const int MinLength = 2;
    public const int MaxLength = 100;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyValueException(nameof(FullName));
        if (value.Length < MinLength)
            throw new TooShortValueException(nameof(FullName), MinLength);
        if (value.Length > MaxLength)
            throw new TooLongValueException(nameof(FullName), MaxLength);
    }
}