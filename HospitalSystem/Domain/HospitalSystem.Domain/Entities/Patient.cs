using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

public class Patient : Entity<Guid>
{
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public InsurancePolicy? InsurancePolicy { get; private set; }
    public string? Allergies { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Patient() { }

    public Patient(FullName name, Email email, PhoneNumber phoneNumber, DateOnly birthDate,
                   InsurancePolicy? insurancePolicy, string? allergies, DateTime createdAt)
        : base(Guid.NewGuid())
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Email = email ?? throw new ArgumentNullValueException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullValueException(nameof(phoneNumber));
        BirthDate = birthDate;
        InsurancePolicy = insurancePolicy;
        Allergies = allergies;
        CreatedAt = createdAt;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
}