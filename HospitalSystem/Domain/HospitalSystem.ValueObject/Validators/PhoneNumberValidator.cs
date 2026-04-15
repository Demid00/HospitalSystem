// Validators/PhoneNumberValidator.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Exceptions;
using System.Text.RegularExpressions;

namespace Hospital.ValueObjects.Validators;

public class PhoneNumberValidator : IValidator<string>
{
    private static readonly Regex PhoneRegex = new(
        @"^(\+7|8)?[\s\-]?\(?[0-9]{3}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$",
        RegexOptions.Compiled);

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        var cleanNumber = Regex.Replace(value, @"[\s\-\(\)]", "");
        if (cleanNumber.Length < 10 || cleanNumber.Length > 12)
            throw new InvalidPhoneNumberFormatException(value);
        if (!PhoneRegex.IsMatch(value))
            throw new InvalidPhoneNumberFormatException(value);
    }
}