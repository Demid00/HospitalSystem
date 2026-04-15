// Validators/EmailValidator.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Exceptions;
using System.Text.RegularExpressions;

namespace Hospital.ValueObjects.Validators;

public class EmailValidator : IValidator<string>
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (!EmailRegex.IsMatch(value))
            throw new InvalidEmailFormatException(value);
    }
}