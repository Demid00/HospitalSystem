using System.Text.RegularExpressions;
using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class PhoneNumberValidator : IValidator<string>
{
    private static readonly Regex PhoneRegex = new(
        @"^(\+7|8)?[\s\-]?\(?[0-9]{3}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$",
        RegexOptions.Compiled);

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyValueException(nameof(PhoneNumber));
        var clean = Regex.Replace(value, @"[\s\-\(\)]", "");
        if (clean.Length < 10 || clean.Length > 12 || !PhoneRegex.IsMatch(value))
            throw new InvalidPhoneNumberFormatException(value);
    }
}