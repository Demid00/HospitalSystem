namespace HospitalSystem.Domain.ValueObjects.Exceptions;

/// <summary>
/// The exception that is thrown when no validation method is specified for the type.
/// </summary>
public class ValidatorNullException : ArgumentNullException
{
    public ValidatorNullException(string typeFullName)
        : base("validator", $"Validator must be specified for type '{typeFullName}'")
    {
    }
}