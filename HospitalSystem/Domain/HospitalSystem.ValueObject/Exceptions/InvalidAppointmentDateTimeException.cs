namespace Hospital.Domain.ValueObjects.Exceptions;

public class InvalidAppointmentDateTimeException(string message)
    : ValueObjectException(message);