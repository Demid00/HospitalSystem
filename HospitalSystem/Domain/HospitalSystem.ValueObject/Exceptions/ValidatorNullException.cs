namespace Hospital.Domain.ValueObjects.Exceptions;

public class ValidatorNullException : ValueObjectException
{
    public ValidatorNullException(string typeFullName)
        : base($"Validator must be specified for type '{typeFullName}'")
    {
    }
}