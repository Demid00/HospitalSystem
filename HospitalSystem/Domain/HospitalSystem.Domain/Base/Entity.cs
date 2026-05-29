namespace Hospital.Domain.Base;

public abstract class Entity<TId>(TId id) where TId : struct, IEquatable<TId>
{
    public TId Id { get; } = id;

    protected Entity() : this(default!) { }

    public override bool Equals(object? obj) => obj is Entity<TId> other && Id.Equals(other.Id);
    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);
}