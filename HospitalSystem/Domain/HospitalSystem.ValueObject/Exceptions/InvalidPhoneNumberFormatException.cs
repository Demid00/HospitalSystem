namespace Hospital.Domain.ValueObjects.Exceptions;

public class InvalidPhoneNumberFormatException(string phone)
    : ValueObjectException($"Phone number '{phone}' is invalid.");