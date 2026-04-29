using Hospital.Domain.Entities;

namespace Hospital.Domain.Exceptions;

public class AppointmentNotFoundException(Guid appointmentId)
    : DomainException($"Appointment {appointmentId} not found.");

public class AppointmentAlreadyExistsException(DateTime dateTime, Guid doctorId)
    : DomainException($"Appointment already exists for doctor {doctorId} at {dateTime}.");

public class AppointmentCannotBeCancelledException(Guid appointmentId, string status)
    : DomainException($"Appointment {appointmentId} cannot be cancelled because status is {status}.");

public class AppointmentCannotBeCompletedException(Guid appointmentId, string status)
    : DomainException($"Appointment {appointmentId} cannot be completed because status is {status}.");

public class CannotBookAppointmentInPastException()
    : DomainException("Cannot book appointment in the past.");

public class CannotRescheduleAppointmentInPastException()
    : DomainException("Cannot reschedule appointment to the past.");

public class AppointmentDoesNotBelongToPatientException(Guid appointmentId, Guid patientId)
    : DomainException($"Appointment {appointmentId} does not belong to patient {patientId}.");