using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Validators;

namespace Hospital.Domain.ValueObjects;

public class CabinetNumber(int value) : ValueObject<int>(new CabinetNumberValidator(), value);