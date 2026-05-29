namespace Hospital.Domain.ValueObjects.Exceptions;

public class InvalidEmailFormatException(string email)
    : ValueObjectException($"Email '{email}' has invalid format.");