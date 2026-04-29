using Hospital.Domain.Entities;

namespace Hospital.Domain.Exceptions;

public class PatientNotFoundException(Guid patientId)
    : DomainException($"Patient with ID {patientId} not found.");

public class PatientAlreadyDeactivatedException(Guid patientId)
    : DomainException($"Patient {patientId} is already deactivated.");