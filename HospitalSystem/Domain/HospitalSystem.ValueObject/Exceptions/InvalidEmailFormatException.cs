namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class InvalidEmailFormatException : FormatException
{
    public InvalidEmailFormatException(string email)
        : base($"Email '{email}' has invalid format.")
    {
    }
}