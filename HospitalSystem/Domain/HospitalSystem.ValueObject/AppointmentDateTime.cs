using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Validators;

namespace Hospital.Domain.ValueObjects;

public class AppointmentDateTime(DateTime value) : ValueObject<DateTime>(new AppointmentDateTimeValidator(), value);