namespace Hospital.ValueObjects.Exceptions;

public class InvalidEmailFormatException : FormatException
{
    public string Email { get; }

    public InvalidEmailFormatException(string email)
        : base($"Email '{email}' has invalid format.") => Email = email;
}