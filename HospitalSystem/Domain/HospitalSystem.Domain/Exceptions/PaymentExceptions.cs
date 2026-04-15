// Exceptions/PaymentExceptions.cs
using Hospital.Domain.Enums;

namespace Hospital.Domain.Exceptions;

public class PaymentAlreadyExistsException : DomainException
{
    public Guid AppointmentId { get; }

    public PaymentAlreadyExistsException(Guid appointmentId)
        : base($"Payment already exists for appointment {appointmentId}.")
    {
        AppointmentId = appointmentId;
    }
}

public class InvalidPaymentStatusTransitionException : DomainException
{
    public Guid PaymentId { get; }
    public PaymentStatus CurrentStatus { get; }
    public PaymentStatus TargetStatus { get; }

    public InvalidPaymentStatusTransitionException(Guid paymentId, PaymentStatus current, PaymentStatus target)
        : base($"Cannot transition payment {paymentId} from {current} to {target}.")
    {
        PaymentId = paymentId;
        CurrentStatus = current;
        TargetStatus = target;
    }
}

public class PaymentNotFoundException : DomainException
{
    public Guid PaymentId { get; }

    public PaymentNotFoundException(Guid paymentId)
        : base($"Payment with ID {paymentId} was not found.") => PaymentId = paymentId;
}

public class InsufficientFundsException : DomainException
{
    public decimal Required { get; }
    public decimal Available { get; }

    public InsufficientFundsException(decimal required, decimal available)
        : base($"Insufficient funds. Required: {required}, Available: {available}")
    {
        Required = required;
        Available = available;
    }
}