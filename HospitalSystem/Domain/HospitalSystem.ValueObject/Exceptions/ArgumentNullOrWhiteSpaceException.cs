namespace HospitalSystem.Domain.ValueObjects.Exceptions;

/// <summary>
/// The exception that is thrown when one of the string arguments is null, 
/// empty or consists only of white-space characters.
/// </summary>
public class ArgumentNullOrWhiteSpaceException : ArgumentException
{
    public ArgumentNullOrWhiteSpaceException(string paramName)
        : base($"Argument \"{paramName}\" value is null, empty or consists only of white-space characters.", paramName)
    {
    }

    public ArgumentNullOrWhiteSpaceException(string paramName, string message)
        : base(message, paramName)
    {
    }
}