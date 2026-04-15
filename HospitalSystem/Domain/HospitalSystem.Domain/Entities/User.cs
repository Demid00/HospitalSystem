// Entities/User.cs
using Hospital.Domain.Enums;
using Hospital.Domain.Exceptions;
using Hospital.ValueObjects;
using Hospital.Domain.Base;

namespace Hospital.Domain.Entities;

public class User : Base.Entity<Guid>
{
    private readonly List<Appointment> _appointments = new();
    private readonly List<Review> _reviews = new();

    public Email Email { get; private set; }
    public FullName FullName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; }

    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

    private User() { }

    public User(Email email, FullName fullName, PhoneNumber phoneNumber, UserRole role)
        : this(Guid.NewGuid(), email, fullName, phoneNumber, role, DateTime.UtcNow) { }

    protected User(Guid id, Email email, FullName fullName, PhoneNumber phoneNumber, UserRole role, DateTime createdAt)
        : base(id)
    {
        Email = email ?? throw new ArgumentNullValueException(nameof(email));
        FullName = fullName ?? throw new ArgumentNullValueException(nameof(fullName));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullValueException(nameof(phoneNumber));
        Role = role;
        IsActive = true;
        CreatedAt = createdAt;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new UserAlreadyDeactivatedException(Id);
        IsActive = false;
    }

    public void UpdateProfile(FullName newFullName, PhoneNumber newPhoneNumber)
    {
        FullName = newFullName ?? throw new ArgumentNullValueException(nameof(newFullName));
        PhoneNumber = newPhoneNumber ?? throw new ArgumentNullValueException(nameof(newPhoneNumber));
    }

    internal void AddAppointment(Appointment appointment)
    {
        _appointments.Add(appointment);
    }

    internal void AddReview(Review review)
    {
        _reviews.Add(review);
    }
}