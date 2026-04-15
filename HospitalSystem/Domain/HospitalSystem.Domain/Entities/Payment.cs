// Entities/Payment.cs
using Hospital.Domain.Enums;
using Hospital.Domain.Exceptions;
using Hospital.ValueObjects;

namespace Hospital.Domain.Entities;

public class Payment : Base.Entity<Guid>
{
    public Guid AppointmentId { get; private set; }
    public Appointment Appointment { get; private set; } = null!;
    public Money Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? PaidAt { get; private set; }
    public string? TransactionId { get; private set; }
    public string? FailureReason { get; private set; }

    private Payment() { }

    public Payment(Appointment appointment, Money amount)
        : this(Guid.NewGuid(), appointment, amount) { }

    protected Payment(Guid id, Appointment appointment, Money amount)
        : base(id)
    {
        Appointment = appointment ?? throw new ArgumentNullValueException(nameof(appointment));
        AppointmentId = appointment.Id;
        Amount = amount ?? throw new ArgumentNullValueException(nameof(amount));
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsPaid(string transactionId)
    {
        if (Status != PaymentStatus.Pending)
            throw new InvalidPaymentStatusTransitionException(Id, Status, PaymentStatus.Paid);

        Status = PaymentStatus.Paid;
        PaidAt = DateTime.UtcNow;
        TransactionId = transactionId ?? throw new ArgumentNullValueException(nameof(transactionId));
    }

    public void MarkAsFailed(string reason)
    {
        if (Status != PaymentStatus.Pending)
            throw new InvalidPaymentStatusTransitionException(Id, Status, PaymentStatus.Failed);

        Status = PaymentStatus.Failed;
        FailureReason = reason;
    }

    public void Refund()
    {
        if (Status != PaymentStatus.Paid)
            throw new InvalidPaymentStatusTransitionException(Id, Status, PaymentStatus.Refunded);

        Status = PaymentStatus.Refunded;
    }
}