// Entities/Schedule.cs
using Hospital.Domain.Exceptions;

namespace Hospital.Domain.Entities;

public class Schedule : Base.Entity<Guid>
{
    public Guid DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;
    public DayOfWeek Weekday { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public bool IsActive { get; private set; }

    private Schedule() { }

    public Schedule(Doctor doctor, DayOfWeek weekday, TimeOnly startTime, TimeOnly endTime)
        : this(Guid.NewGuid(), doctor, weekday, startTime, endTime) { }

    protected Schedule(Guid id, Doctor doctor, DayOfWeek weekday, TimeOnly startTime, TimeOnly endTime)
        : base(id)
    {
        Doctor = doctor ?? throw new ArgumentNullValueException(nameof(doctor));
        DoctorId = doctor.Id;
        Weekday = weekday;

        if (startTime >= endTime)
            throw new InvalidScheduleTimeException(startTime, endTime);

        StartTime = startTime;
        EndTime = endTime;
        IsActive = true;
    }

    public void UpdateTime(TimeOnly newStartTime, TimeOnly newEndTime)
    {
        if (newStartTime >= newEndTime)
            throw new InvalidScheduleTimeException(newStartTime, newEndTime);

        StartTime = newStartTime;
        EndTime = newEndTime;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public bool IsTimeWithinSchedule(TimeOnly time)
    {
        return time >= StartTime && time <= EndTime;
    }

    public bool OverlapsWith(Schedule other)
    {
        if (Weekday != other.Weekday) return false;
        return StartTime < other.EndTime && other.StartTime < EndTime;
    }
}