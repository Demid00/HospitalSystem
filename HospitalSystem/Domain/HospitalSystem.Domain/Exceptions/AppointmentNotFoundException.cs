namespace Hospital.Domain.Exceptions;

public class AppointmentNotFoundException(Guid id) : DomainException($"Appointment {id} not found.");