using Hospital.Domain.Enums;


namespace Hospital.Domain.Exceptions;

public class AppointmentCannotBeCompletedException(Guid id, AppointmentStatus status)
    : DomainException($"Appointment {id} cannot be completed in status {status}.");