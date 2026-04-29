using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Validators;

namespace Hospital.Domain.ValueObjects;

public class PhoneNumber(string value) : ValueObject<string>(new PhoneNumberValidator(), value);