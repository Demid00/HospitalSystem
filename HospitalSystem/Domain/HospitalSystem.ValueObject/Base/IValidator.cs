namespace Hospital.Domain.ValueObjects.Base;

/// <summary>
/// Defines a method that implements the validation of the object.
/// </summary>
public interface IValidator<in T>
{
    void Validate(T value);
}