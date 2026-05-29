using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Validators;

namespace Hospital.Domain.ValueObjects;

public class Treatment(string? value) : ValueObject<string?>(new NullableNotEmptyValidator(), value);