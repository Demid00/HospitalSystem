using Hospital.Domain.Base;
using Hospital.Domain.Enums;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents a payment for an appointment.
/// </summary>
public class Payment : Entity<Guid>
{
    public Guid AppointmentId { get; }
    public Appointment Appointment { get; private set; } = null!;
    public Money Amount { get; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? PaidAt { get; private set; }
    public string TransactionId { get; private set; } = null!;

    private Payment() { }

    internal Payment(Appointment appointment, Money amount, string transactionId)
        : base(Guid.NewGuid())
    {
        Appointment = appointment ?? throw new ArgumentNullValueException(nameof(appointment));
        AppointmentId = appointment.Id;
        Amount = amount ?? throw new ArgumentNullValueException(nameof(amount));
        TransactionId = transactionId ?? throw new ArgumentNullValueException(nameof(transactionId));
        Status = PaymentStatus.Paid;
        CreatedAt = DateTime.UtcNow;
        PaidAt = DateTime.UtcNow;
    }
}