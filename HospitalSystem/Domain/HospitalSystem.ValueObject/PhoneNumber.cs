// PhoneNumber.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Validators;

namespace Hospital.ValueObjects;

public class PhoneNumber : ValueObject<string>
{
    public PhoneNumber(string value) : base(new PhoneNumberValidator(), value) { }
}