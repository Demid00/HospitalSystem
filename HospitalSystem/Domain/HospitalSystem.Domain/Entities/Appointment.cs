using Hospital.Domain.Base;
using Hospital.Domain.Enums;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

public class Appointment : AggregateRoot<Guid>
{
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; }
    public AppointmentDateTime DateTime { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public CancellationReason? CancellationReason { get; private set; }

    private Appointment() { }

    public Appointment(Guid patientId, Guid doctorId, AppointmentDateTime dateTime, DateTime createdAt)
        : base(Guid.NewGuid())
    {
        if (patientId == Guid.Empty) throw new ArgumentNullValueException(nameof(patientId));
        if (doctorId == Guid.Empty) throw new ArgumentNullValueException(nameof(doctorId));
        PatientId = patientId;
        DoctorId = doctorId;
        DateTime = dateTime;
        CreatedAt = createdAt;
        Status = AppointmentStatus.Booked;
    }

    public void Cancel(CancellationReason? reason, DateTime cancelledAt)
    {
        if (Status != AppointmentStatus.Booked)
            throw new AppointmentCannotBeCancelledException(Id, Status);
        Status = AppointmentStatus.Cancelled;
        CancelledAt = cancelledAt;
        CancellationReason = reason;
        AddDomainEvent(new AppointmentCancelledEvent(Id, reason?.Value));
    }

    public void Complete(DateTime completedAt)
    {
        if (Status != AppointmentStatus.Booked)
            throw new AppointmentCannotBeCompletedException(Id, Status);
        Status = AppointmentStatus.Completed;
        CompletedAt = completedAt;
        AddDomainEvent(new AppointmentCompletedEvent(Id));
    }
}

public record AppointmentCancelledEvent(Guid AppointmentId, string? Reason) : IDomainEvent;
public record AppointmentCompletedEvent(Guid AppointmentId) : IDomainEvent;