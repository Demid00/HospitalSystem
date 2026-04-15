// Exceptions/SalaryExceptions.cs
namespace Hospital.Domain.Exceptions;

public class SalaryAlreadyPaidException : DomainException
{
    public Guid SalaryId { get; }

    public SalaryAlreadyPaidException(Guid salaryId)
        : base($"Salary {salaryId} has already been paid.") => SalaryId = salaryId;
}

public class InvalidYearException : DomainException
{
    public int Year { get; }

    public InvalidYearException(int year)
        : base($"Year {year} is invalid. Must be between 2000 and {DateTime.UtcNow.Year + 1}.")
    {
        Year = year;
    }
}

public class InvalidMonthException : DomainException
{
    public int Month { get; }

    public InvalidMonthException(int month)
        : base($"Month {month} is invalid. Must be between 1 and 12.") => Month = month;
}