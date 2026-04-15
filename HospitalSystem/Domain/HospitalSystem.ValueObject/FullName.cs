// FullName.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Validators;

namespace Hospital.ValueObjects;

public class FullName : ValueObject<string>
{
    public FullName(string value) : base(new FullNameValidator(), value) { }
}