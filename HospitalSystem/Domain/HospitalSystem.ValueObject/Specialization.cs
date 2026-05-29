using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Validators;

namespace Hospital.Domain.ValueObjects;

public class Specialization(string value) : ValueObject<string>(new SpecializationValidator(), value);