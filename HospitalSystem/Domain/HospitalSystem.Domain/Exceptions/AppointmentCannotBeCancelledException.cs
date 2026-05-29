using Hospital.Domain.Enums;

namespace Hospital.Domain.Exceptions;

public class AppointmentCannotBeCancelledException(Guid id, AppointmentStatus status)
    : DomainException($"Appointment {id} cannot be cancelled in status {status}.");