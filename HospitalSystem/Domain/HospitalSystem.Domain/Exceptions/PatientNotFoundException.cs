namespace Hospital.Domain.Exceptions;

public class PatientNotFoundException(Guid id) : DomainException($"Patient {id} not found.");