using Hospital.Domain.Base;
using Hospital.Domain.Enums;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents a patient at the hospital.
/// </summary>
public class Patient : Entity<Guid>
{
    private readonly List<Appointment> _appointments = [];

    public FullName Name { get; }
    public Email Email { get; }
    public PhoneNumber PhoneNumber { get; }
    public DateOnly BirthDate { get; }
    public string? InsurancePolicy { get; private set; }
    public string? Allergies { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; }

    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public bool HasActiveAppointments => _appointments.Any(a => a.Status == AppointmentStatus.Booked);
    public decimal TotalSpent => _appointments
        .Where(a => a.Status == AppointmentStatus.Completed && a.Payment?.Status == PaymentStatus.Paid)
        .Sum(a => a.Price.Value);

    private Patient() { }

    public Patient(FullName name, Email email, PhoneNumber phoneNumber, DateOnly birthDate,
                   string? insurancePolicy = null, string? allergies = null)
        : base(Guid.NewGuid())
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Email = email ?? throw new ArgumentNullValueException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullValueException(nameof(phoneNumber));
        BirthDate = birthDate;
        InsurancePolicy = insurancePolicy;
        Allergies = allergies;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Books an appointment with a doctor.
    /// </summary>
    public Appointment BookAppointment(Doctor doctor, DateTime dateTime, Money price)
    {
        if (doctor == null) throw new ArgumentNullValueException(nameof(doctor));
        if (price == null) throw new ArgumentNullValueException(nameof(price));
        if (dateTime <= DateTime.UtcNow)
            throw new CannotBookAppointmentInPastException();
        if (!doctor.IsAvailableAt(dateTime))
            throw new DoctorNotAvailableException(doctor.Id, dateTime);
        if (_appointments.Any(a => a.DoctorId == doctor.Id && a.DateTime == dateTime && a.Status != AppointmentStatus.Cancelled))
            throw new AppointmentAlreadyExistsException(dateTime, doctor.Id);

        var appointment = new Appointment(this, doctor, dateTime, price);
        _appointments.Add(appointment);
        return appointment;
    }

    /// <summary>
    /// Cancels an appointment.
    /// </summary>
    public bool CancelAppointment(Guid appointmentId, string? reason = null)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == appointmentId)
            ?? throw new AppointmentNotFoundException(appointmentId);

        return appointment.SetCancel(reason);
    }

    /// <summary>
    /// Completes an appointment.
    /// </summary>
    public bool CompleteAppointment(Guid appointmentId)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == appointmentId)
            ?? throw new AppointmentNotFoundException(appointmentId);

        return appointment.SetComplete();
    }

    /// <summary>
    /// Adds a payment to an appointment.
    /// </summary>
    public Payment AddPayment(Guid appointmentId, Money amount, string transactionId)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == appointmentId)
            ?? throw new AppointmentNotFoundException(appointmentId);

        return appointment.AddPayment(amount, transactionId);
    }

    /// <summary>
    /// Adds a medical record to an appointment.
    /// </summary>
    public MedicalRecord AddMedicalRecord(Guid appointmentId, string complaints, string diagnosis,
                                          string? treatment = null, string? conclusion = null)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == appointmentId)
            ?? throw new AppointmentNotFoundException(appointmentId);

        return appointment.AddMedicalRecord(complaints, diagnosis, treatment, conclusion);
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}