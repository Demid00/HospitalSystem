using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Validators;

namespace Hospital.Domain.ValueObjects;

public class FullName(string value) : ValueObject<string>(new FullNameValidator(), value);