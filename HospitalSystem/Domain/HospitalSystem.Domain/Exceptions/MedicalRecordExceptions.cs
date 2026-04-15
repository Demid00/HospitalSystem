// Exceptions/MedicalRecordExceptions.cs
namespace Hospital.Domain.Exceptions;

public class MedicalRecordAlreadyExistsException : DomainException
{
    public Guid AppointmentId { get; }

    public MedicalRecordAlreadyExistsException(Guid appointmentId)
        : base($"Medical record already exists for appointment {appointmentId}.")
    {
        AppointmentId = appointmentId;
    }
}

public class MedicalRecordNotFoundException : DomainException
{
    public Guid MedicalRecordId { get; }

    public MedicalRecordNotFoundException(Guid medicalRecordId)
        : base($"Medical record with ID {medicalRecordId} was not found.")
    {
        MedicalRecordId = medicalRecordId;
    }
}

public class ProcedureAlreadyCompletedException : DomainException
{
    public Guid PrescribedProcedureId { get; }

    public ProcedureAlreadyCompletedException(Guid prescribedProcedureId)
        : base($"Prescribed procedure {prescribedProcedureId} has already been completed.")
    {
        PrescribedProcedureId = prescribedProcedureId;
    }
}

public class InvalidProcedureDurationException : DomainException
{
    public int DurationMinutes { get; }

    public InvalidProcedureDurationException(int durationMinutes)
        : base($"Procedure duration {durationMinutes} minutes is invalid. Must be greater than 0.")
    {
        DurationMinutes = durationMinutes;
    }
}