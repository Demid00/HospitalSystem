namespace Hospital.Domain.Exceptions;

public class DoctorNotFoundException(Guid id) : DomainException($"Doctor {id} not found.");