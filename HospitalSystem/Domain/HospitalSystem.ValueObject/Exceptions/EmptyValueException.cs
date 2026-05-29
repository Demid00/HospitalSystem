namespace Hospital.Domain.ValueObjects.Exceptions;

public class EmptyValueException(string valueName)
    : ValueObjectException($"{valueName} cannot be null, empty or whitespace.");