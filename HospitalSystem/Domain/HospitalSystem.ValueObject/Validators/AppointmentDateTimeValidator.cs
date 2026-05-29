using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects.Validators;

public class AppointmentDateTimeValidator : IValidator<DateTime>
{
    public void Validate(DateTime value)
    {
        if (value <= DateTime.UtcNow)
            throw new InvalidAppointmentDateTimeException("Appointment date must be in the future.");
        var time = TimeOnly.FromDateTime(value);
        if (time < new TimeOnly(8, 0) || time > new TimeOnly(18, 0))
            throw new InvalidAppointmentDateTimeException("Appointment time must be between 08:00 and 18:00.");
    }
}