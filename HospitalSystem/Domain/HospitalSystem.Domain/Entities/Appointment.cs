using Hospital.Domain.Base;
using Hospital.Domain.Enums;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents an appointment between a patient and a doctor.
/// </summary>
public class Appointment : Entity<Guid>
{
    public Guid PatientId { get; }
    public Patient Patient { get; private set; } = null!;
    public Guid DoctorId { get; }
    public Doctor Doctor { get; private set; } = null!;
    public DateTime DateTime { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public Money Price { get; }
    public DateTime CreatedAt { get; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public string? CancellationReason { get; private set; }

    public Payment? Payment { get; private set; }
    public MedicalRecord? MedicalRecord { get; private set; }

    private Appointment() { }

    internal Appointment(Patient patient, Doctor doctor, DateTime dateTime, Money price)
        : base(Guid.NewGuid())
    {
        Patient = patient ?? throw new ArgumentNullValueException(nameof(patient));
        PatientId = patient.Id;
        Doctor = doctor ?? throw new ArgumentNullValueException(nameof(doctor));
        DoctorId = doctor.Id;
        DateTime = dateTime;
        Price = price ?? throw new ArgumentNullValueException(nameof(price));
        Status = AppointmentStatus.Booked;
        CreatedAt = DateTime.UtcNow;
    }

    internal bool SetCancel(string? reason = null)
    {
        if (Status != AppointmentStatus.Booked)
            throw new AppointmentCannotBeCancelledException(Id, Status.ToString());

        Status = AppointmentStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
        CancellationReason = reason;
        return true;
    }

    internal bool SetComplete()
    {
        if (Status != AppointmentStatus.Booked)
            throw new AppointmentCannotBeCompletedException(Id, Status.ToString());

        Status = AppointmentStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        return true;
    }

    internal Payment AddPayment(Money amount, string transactionId)
    {
        if (Payment != null)
            throw new InvalidOperationException($"Payment already exists for appointment {Id}");

        var payment = new Payment(this, amount, transactionId);
        Payment = payment;
        return payment;
    }

    internal MedicalRecord AddMedicalRecord(string complaints, string diagnosis,
                                            string? treatment = null, string? conclusion = null)
    {
        if (MedicalRecord != null)
            throw new InvalidOperationException($"Medical record already exists for appointment {Id}");

        var record = new MedicalRecord(this, complaints, diagnosis, treatment, conclusion);
        MedicalRecord = record;
        return record;
    }
}