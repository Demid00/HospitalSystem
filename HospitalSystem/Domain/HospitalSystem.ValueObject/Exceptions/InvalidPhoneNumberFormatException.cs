namespace HospitalSystem.Domain.ValueObjects.Exceptions;

public class InvalidPhoneNumberFormatException : FormatException
{
    public InvalidPhoneNumberFormatException(string phoneNumber)
        : base($"Phone number '{phoneNumber}' has invalid format.")
    {
    }
}