namespace Hospital.Domain.ValueObjects.Exceptions;

public class TooShortValueException(string valueName, int minLength)
    : ValueObjectException($"{valueName} length must be at least {minLength}.");