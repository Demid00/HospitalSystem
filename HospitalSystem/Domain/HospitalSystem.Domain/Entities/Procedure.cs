// Entities/Procedure.cs
using Hospital.Domain.Exceptions;
using Hospital.ValueObjects;

namespace Hospital.Domain.Entities;

public class Procedure : Base.Entity<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Money Price { get; private set; }
    public int EstimatedDurationMinutes { get; private set; }
    public bool IsActive { get; private set; }

    private Procedure() { }

    public Procedure(string name, Money price, int estimatedDurationMinutes, string? description = null)
        : this(Guid.NewGuid(), name, price, estimatedDurationMinutes, description) { }

    protected Procedure(Guid id, string name, Money price, int estimatedDurationMinutes, string? description)
        : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Price = price ?? throw new ArgumentNullValueException(nameof(price));

        if (estimatedDurationMinutes <= 0)
            throw new InvalidProcedureDurationException(estimatedDurationMinutes);

        EstimatedDurationMinutes = estimatedDurationMinutes;
        Description = description;
        IsActive = true;
    }

    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullValueException(nameof(newPrice));
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void UpdateInfo(string name, string? description, int estimatedDurationMinutes)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Description = description;

        if (estimatedDurationMinutes <= 0)
            throw new InvalidProcedureDurationException(estimatedDurationMinutes);

        EstimatedDurationMinutes = estimatedDurationMinutes;
    }
}