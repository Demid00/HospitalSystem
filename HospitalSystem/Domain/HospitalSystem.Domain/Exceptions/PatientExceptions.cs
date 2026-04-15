// Exceptions/PatientExceptions.cs
namespace Hospital.Domain.Exceptions;

public class PatientNotFoundException : DomainException
{
    public Guid PatientId { get; }

    public PatientNotFoundException(Guid patientId)
        : base($"Patient with ID {patientId} was not found.") => PatientId = patientId;
}

public class PatientAlreadyDeactivatedException : DomainException
{
    public Guid PatientId { get; }

    public PatientAlreadyDeactivatedException(Guid patientId)
        : base($"Patient {patientId} is already deactivated.") => PatientId = patientId;
}