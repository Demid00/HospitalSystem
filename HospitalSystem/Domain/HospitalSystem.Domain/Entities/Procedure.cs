using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents a medical procedure or examination.
/// </summary>
public class Procedure : Entity<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Money Price { get; private set; }
    public int EstimatedDurationMinutes { get; private set; }
    public bool IsActive { get; private set; }

    private Procedure() { }

    public Procedure(string name, Money price, int estimatedDurationMinutes, string? description = null)
        : base(Guid.NewGuid())
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Price = price ?? throw new ArgumentNullValueException(nameof(price));

        if (estimatedDurationMinutes <= 0)
            throw new ArgumentException("Duration must be positive", nameof(estimatedDurationMinutes));

        EstimatedDurationMinutes = estimatedDurationMinutes;
        Description = description;
        IsActive = true;
    }

    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullValueException(nameof(newPrice));
    }

    public void Deactivate() => IsActive = false;
}