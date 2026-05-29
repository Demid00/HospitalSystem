using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Validators;

namespace Hospital.Domain.ValueObjects;

public class CancellationReason(string value) : ValueObject<string>(new NotEmptyValidator(), value);