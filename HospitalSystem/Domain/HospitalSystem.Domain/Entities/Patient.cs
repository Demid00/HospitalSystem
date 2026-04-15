// Entities/Patient.cs
using Hospital.Domain.Exceptions;
using Hospital.ValueObjects;
using Hospital.Domain.Base;

namespace Hospital.Domain.Entities;

public class Patient : Base.Entity<Guid>
{
    private readonly List<Appointment> _appointments = new();
    private readonly List<Review> _reviews = new();

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public DateOnly BirthDate { get; private set; }
    public string? InsurancePolicy { get; private set; }
    public string? Allergies { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

    private Patient() { }

    public Patient(User user, DateOnly birthDate, string? insurancePolicy = null, string? allergies = null)
        : this(Guid.NewGuid(), user, birthDate, insurancePolicy, allergies) { }

    protected Patient(Guid id, User user, DateOnly birthDate, string? insurancePolicy, string? allergies)
        : base(id)
    {
        User = user ?? throw new ArgumentNullValueException(nameof(user));
        UserId = user.Id;
        BirthDate = birthDate;
        InsurancePolicy = insurancePolicy;
        Allergies = allergies;
        IsActive = true;
    }

    public Appointment BookAppointment(Doctor doctor, DateTime appointmentDateTime, Money price)
    {
        if (!doctor.IsAvailableAt(appointmentDateTime))
            throw new AppointmentTimeUnavailableException(doctor.Id, appointmentDateTime);

        var appointment = new Appointment(this, doctor, appointmentDateTime, price);
        _appointments.Add(appointment);
        doctor.AddAppointment(appointment);
        User.AddAppointment(appointment);
        return appointment;
    }

    public Review WriteReview(Doctor doctor, int rating, string? comment = null)
    {
        var review = new Review(this, doctor, rating, comment);
        _reviews.Add(review);
        doctor.AddReview(review);
        User.AddReview(review);
        return review;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void UpdateMedicalInfo(string? insurancePolicy, string? allergies)
    {
        if (!string.IsNullOrWhiteSpace(insurancePolicy))
            InsurancePolicy = insurancePolicy;
        if (!string.IsNullOrWhiteSpace(allergies))
            Allergies = allergies;
    }
}