// Entities/Salary.cs
using Hospital.Domain.Exceptions;
using Hospital.ValueObjects;

namespace Hospital.Domain.Entities;

public class Salary : Base.Entity<Guid>
{
    public Guid DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;
    public int Year { get; private set; }
    public int Month { get; private set; }
    public Money Amount { get; private set; }
    public bool IsPaid { get; private set; }
    public DateTime? PaidAt { get; private set; }
    public string? TransactionReference { get; private set; }

    private Salary() { }

    public Salary(Doctor doctor, int year, int month, Money amount)
        : this(Guid.NewGuid(), doctor, year, month, amount) { }

    protected Salary(Guid id, Doctor doctor, int year, int month, Money amount)
        : base(id)
    {
        Doctor = doctor ?? throw new ArgumentNullValueException(nameof(doctor));
        DoctorId = doctor.Id;

        if (year < 2000 || year > DateTime.UtcNow.Year + 1)
            throw new InvalidYearException(year);

        if (month < 1 || month > 12)
            throw new InvalidMonthException(month);

        Year = year;
        Month = month;
        Amount = amount ?? throw new ArgumentNullValueException(nameof(amount));
        IsPaid = false;
    }

    public void MarkAsPaid(string transactionReference)
    {
        if (IsPaid)
            throw new SalaryAlreadyPaidException(Id);

        IsPaid = true;
        PaidAt = DateTime.UtcNow;
        TransactionReference = transactionReference;
    }
}