// Exceptions/AppointmentExceptions.cs
using Hospital.Domain.Enums;

namespace Hospital.Domain.Exceptions;

public class AppointmentCannotBeCancelledException : DomainException
{
    public Guid AppointmentId { get; }
    public AppointmentStatus CurrentStatus { get; }

    public AppointmentCannotBeCancelledException(Guid appointmentId, AppointmentStatus status)
        : base($"Appointment {appointmentId} cannot be cancelled because its status is {status}.")
    {
        AppointmentId = appointmentId;
        CurrentStatus = status;
    }
}

public class AppointmentCannotBeCompletedException : DomainException
{
    public Guid AppointmentId { get; }
    public AppointmentStatus CurrentStatus { get; }

    public AppointmentCannotBeCompletedException(Guid appointmentId, AppointmentStatus status)
        : base($"Appointment {appointmentId} cannot be completed because its status is {status}.")
    {
        AppointmentId = appointmentId;
        CurrentStatus = status;
    }
}

public class AppointmentTimeUnavailableException : DomainException
{
    public DateTime RequestedTime { get; }
    public Guid DoctorId { get; }

    public AppointmentTimeUnavailableException(Guid doctorId, DateTime requestedTime)
        : base($"Doctor {doctorId} is not available at {requestedTime}.")
    {
        DoctorId = doctorId;
        RequestedTime = requestedTime;
    }
}

public class AppointmentAlreadyExistsException : DomainException
{
    public Guid PatientId { get; }
    public Guid DoctorId { get; }
    public DateTime DateTime { get; }

    public AppointmentAlreadyExistsException(Guid patientId, Guid doctorId, DateTime dateTime)
        : base($"Appointment already exists for patient {patientId} with doctor {doctorId} at {dateTime}.")
    {
        PatientId = patientId;
        DoctorId = doctorId;
        DateTime = dateTime;
    }
}