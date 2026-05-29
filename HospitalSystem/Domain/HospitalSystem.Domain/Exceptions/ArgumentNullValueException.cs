namespace Hospital.Domain.Exceptions;

public class ArgumentNullValueException(string paramName)
    : DomainException($"Argument '{paramName}' is null.");