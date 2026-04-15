namespace Hospital.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException : ArgumentNullException
{
    public ArgumentNullOrWhiteSpaceException(string paramName)
        : base(paramName, $"The \"{paramName}\" mustn't be null, empty or consists only of white-space characters.") { }
}