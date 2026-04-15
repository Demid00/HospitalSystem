namespace Hospital.ValueObjects.Exceptions;

public class InvalidPhoneNumberFormatException : FormatException
{
    public string PhoneNumber { get; }

    public InvalidPhoneNumberFormatException(string phoneNumber)
        : base($"Phone number '{phoneNumber}' has invalid format.") => PhoneNumber = phoneNumber;
}