// Exceptions/DomainException.cs
namespace Hospital.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}