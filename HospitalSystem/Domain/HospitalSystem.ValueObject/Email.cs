// Email.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Validators;

namespace Hospital.ValueObjects;

public class Email : ValueObject<string>
{
    public Email(string value) : base(new EmailValidator(), value) { }
}