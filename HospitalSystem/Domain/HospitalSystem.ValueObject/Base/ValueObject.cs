using Hospital.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Base;

public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
{
    public T Value { get; }

    protected ValueObject(IValidator<T> validator, T value)
    {
        validator.Validate(value);
        Value = value;
    }

    public override string ToString() => Value?.ToString() ?? GetType().ToString();
    public override int GetHashCode() => Value?.GetHashCode() ?? 0;
    public override bool Equals(object? other) => Equals(other as ValueObject<T>);
    public bool Equals(ValueObject<T>? other) =>
        other is not null && GetType() == other.GetType() && EqualityComparer<T>.Default.Equals(Value, other.Value);
    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right) => Equals(left, right);
    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right) => !(left == right);
}