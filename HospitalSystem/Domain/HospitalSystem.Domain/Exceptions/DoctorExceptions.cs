using Hospital.Domain.Entities;

namespace Hospital.Domain.Exceptions;

public class DoctorNotFoundException(Guid doctorId)
    : DomainException($"Doctor with ID {doctorId} not found.");

public class DoctorNotAvailableException(Guid doctorId, DateTime dateTime)
    : DomainException($"Doctor {doctorId} is not available at {dateTime}.");

public class DoctorAlreadyDeactivatedException(Guid doctorId)
    : DomainException($"Doctor {doctorId} is already deactivated.");

public class ScheduleOverlapException(DayOfWeek day, TimeOnly start, TimeOnly end)
    : DomainException($"Schedule overlap on {day} between {start} and {end}.");

public class InvalidScheduleTimeException(TimeOnly start, TimeOnly end)
    : DomainException($"Start time {start} must be less than end time {end}.");