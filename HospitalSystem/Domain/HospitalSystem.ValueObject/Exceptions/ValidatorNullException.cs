namespace Hospital.ValueObjects.Exceptions;

public class ValidatorNullException : ArgumentNullException
{
    public ValidatorNullException(string paramName)
        : base(paramName, $"Validator \"{paramName}\" must be specified for type.") { }
}