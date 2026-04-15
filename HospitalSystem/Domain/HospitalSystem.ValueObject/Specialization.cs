// Specialization.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Validators;

namespace Hospital.ValueObjects;

public class Specialization : ValueObject<string>
{
    public Specialization(string value) : base(new SpecializationValidator(), value) { }
}