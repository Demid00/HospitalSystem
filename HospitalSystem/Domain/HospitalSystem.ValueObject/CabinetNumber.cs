// CabinetNumber.cs
using Hospital.ValueObjects.Base;
using Hospital.ValueObjects.Validators;

namespace Hospital.ValueObjects;

public class CabinetNumber : ValueObject<int>
{
    public CabinetNumber(int value) : base(new CabinetNumberValidator(), value) { }
}