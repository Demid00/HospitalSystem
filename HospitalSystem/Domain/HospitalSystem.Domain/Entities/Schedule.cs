using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents a doctor's working schedule.
/// </summary>
public class Schedule : Entity<Guid>
{
    public Guid DoctorId { get; }
    public Doctor Doctor { get; private set; } = null!;
    public DayOfWeek Weekday { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }
    public bool IsActive { get; private set; }

    private Schedule() { }

    internal Schedule(Doctor doctor, DayOfWeek weekday, TimeOnly startTime, TimeOnly endTime)
        : base(Guid.NewGuid())
    {
        Doctor = doctor ?? throw new ArgumentNullValueException(nameof(doctor));
        DoctorId = doctor.Id;
        Weekday = weekday;
        StartTime = startTime;
        EndTime = endTime;
        IsActive = true;
    }

    public bool ContainsTime(TimeOnly time) => time >= StartTime && time <= EndTime;

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}