using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

public class Doctor : Entity<Guid>
{
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Specialization Specialization { get; private set; }
    public CabinetNumber CabinetNumber { get; private set; }
    public Description? Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Doctor() { }

    public Doctor(FullName name, Email email, PhoneNumber phoneNumber, Specialization specialization,
                  CabinetNumber cabinetNumber, Description? description, DateTime createdAt)
        : base(Guid.NewGuid())
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Email = email ?? throw new ArgumentNullValueException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullValueException(nameof(phoneNumber));
        Specialization = specialization ?? throw new ArgumentNullValueException(nameof(specialization));
        CabinetNumber = cabinetNumber ?? throw new ArgumentNullValueException(nameof(cabinetNumber));
        Description = description;
        CreatedAt = createdAt;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
}