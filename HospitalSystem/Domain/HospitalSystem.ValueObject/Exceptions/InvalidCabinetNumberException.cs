namespace Hospital.Domain.ValueObjects.Exceptions;

public class InvalidCabinetNumberException(int value)
    : ValueObjectException($"Cabinet number {value} is out of range (1-999).");