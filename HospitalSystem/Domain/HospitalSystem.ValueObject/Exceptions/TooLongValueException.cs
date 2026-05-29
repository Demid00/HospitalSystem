namespace Hospital.Domain.ValueObjects.Exceptions;

public class TooLongValueException(string valueName, int maxLength)
    : ValueObjectException($"{valueName} length must not exceed {maxLength}.");