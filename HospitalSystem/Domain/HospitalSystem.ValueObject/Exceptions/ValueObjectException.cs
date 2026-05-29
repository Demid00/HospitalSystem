namespace Hospital.Domain.ValueObjects.Exceptions;

/// <summary>
/// Базовое исключение для всех ошибок валидации Value Objects.
/// </summary>
public abstract class ValueObjectException : Exception
{
    protected ValueObjectException(string message) : base(message) { }
}