// Exceptions/DoctorExceptions.cs
namespace Hospital.Domain.Exceptions;

public class DoctorAlreadyDeactivatedException : DomainException
{
    public Guid DoctorId { get; }

    public DoctorAlreadyDeactivatedException(Guid doctorId)
        : base($"Doctor {doctorId} is already deactivated.") => DoctorId = doctorId;
}

public class DoctorNotFoundException : DomainException
{
    public Guid DoctorId { get; }

    public DoctorNotFoundException(Guid doctorId)
        : base($"Doctor with ID {doctorId} was not found.") => DoctorId = doctorId;
}

public class ScheduleDoctorMismatchException : DomainException
{
    public Guid ScheduleId { get; }
    public Guid DoctorId { get; }

    public ScheduleDoctorMismatchException(Guid scheduleId, Guid doctorId)
        : base($"Schedule {scheduleId} does not belong to doctor {doctorId}.")
    {
        ScheduleId = scheduleId;
        DoctorId = doctorId;
    }
}