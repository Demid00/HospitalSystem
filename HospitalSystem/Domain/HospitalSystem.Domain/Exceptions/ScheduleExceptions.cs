// Exceptions/ScheduleExceptions.cs
namespace Hospital.Domain.Exceptions;

public class InvalidScheduleTimeException : DomainException
{
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public InvalidScheduleTimeException(TimeOnly startTime, TimeOnly endTime)
        : base($"Start time {startTime} cannot be greater than or equal to end time {endTime}.")
    {
        StartTime = startTime;
        EndTime = endTime;
    }
}

public class ScheduleOverlapException : DomainException
{
    public DayOfWeek Weekday { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public ScheduleOverlapException(DayOfWeek weekday, TimeOnly startTime, TimeOnly endTime)
        : base($"Schedule overlap detected for {weekday} between {startTime} and {endTime}.")
    {
        Weekday = weekday;
        StartTime = startTime;
        EndTime = endTime;
    }
}