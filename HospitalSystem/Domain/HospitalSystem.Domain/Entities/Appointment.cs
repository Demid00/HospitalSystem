// Entities/Appointment.cs
using Hospital.Domain.Enums;
using Hospital.Domain.Exceptions;
using Hospital.ValueObjects;
using Hospital.Domain.Base;

namespace Hospital.Domain.Entities;

public class Appointment : Base.Entity<Guid>
{
    public Guid PatientId { get; private set; }
    public Patient Patient { get; private set; } = null!;
    public Guid DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;
    public DateTime AppointmentDateTime { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public Money Price { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public string? CancellationReason { get; private set; }

    public MedicalRecord? MedicalRecord { get; private set; }
    public Payment? Payment { get; private set; }

    private Appointment() { }

    public Appointment(Patient patient, Doctor doctor, DateTime appointmentDateTime, Money price)
        : this(Guid.NewGuid(), patient, doctor, appointmentDateTime, price) { }

    protected Appointment(Guid id, Patient patient, Doctor doctor, DateTime appointmentDateTime, Money price)
        : base(id)
    {
        Patient = patient ?? throw new ArgumentNullValueException(nameof(patient));
        PatientId = patient.Id;
        Doctor = doctor ?? throw new ArgumentNullValueException(nameof(doctor));
        DoctorId = doctor.Id;
        AppointmentDateTime = appointmentDateTime;
        Status = AppointmentStatus.Booked;
        Price = price ?? throw new ArgumentNullValueException(nameof(price));
        CreatedAt = DateTime.UtcNow;
    }

    public void Cancel(string? reason = null)
    {
        if (Status != AppointmentStatus.Booked)
            throw new AppointmentCannotBeCancelledException(Id, Status);

        Status = AppointmentStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
        CancellationReason = reason;
    }

    public void Complete()
    {
        if (Status != AppointmentStatus.Booked)
            throw new AppointmentCannotBeCompletedException(Id, Status);

        Status = AppointmentStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void MarkAsNoShow()
    {
        if (Status != AppointmentStatus.Booked)
            throw new AppointmentCannotBeCancelledException(Id, Status);

        Status = AppointmentStatus.NoShow;
    }

    public void AddMedicalRecord(MedicalRecord record)
    {
        if (MedicalRecord != null)
            throw new MedicalRecordAlreadyExistsException(Id);

        MedicalRecord = record ?? throw new ArgumentNullValueException(nameof(record));
        Doctor.AddMedicalRecord(record);
    }

    public void AddPayment(Payment payment)
    {
        if (Payment != null)
            throw new PaymentAlreadyExistsException(Id);

        Payment = payment ?? throw new ArgumentNullValueException(nameof(payment));
    }

    public void Reschedule(DateTime newDateTime)
    {
        if (Status != AppointmentStatus.Booked)
            throw new AppointmentCannotBeCancelledException(Id, Status);

        if (!Doctor.IsAvailableAt(newDateTime))
            throw new AppointmentTimeUnavailableException(DoctorId, newDateTime);

        AppointmentDateTime = newDateTime;
    }
}