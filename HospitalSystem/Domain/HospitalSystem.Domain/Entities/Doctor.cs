using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents a doctor at the hospital.
/// </summary>
public class Doctor : Entity<Guid>
{
    private readonly List<Schedule> _schedules = [];
    private readonly List<Review> _reviews = [];
    private readonly List<Template> _templates = [];
    private readonly List<Appointment> _appointments = [];

    public FullName Name { get; }
    public Email Email { get; }
    public PhoneNumber PhoneNumber { get; }
    public Specialization Specialization { get; }
    public CabinetNumber CabinetNumber { get; private set; }
    public Money ConsultationPrice { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; }

    public IReadOnlyCollection<Schedule> Schedules => _schedules.AsReadOnly();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
    public IReadOnlyCollection<Template> Templates => _templates.AsReadOnly();
    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();

    public double AverageRating => _reviews.Any() ? _reviews.Average(r => r.Rating) : 0;

    private Doctor() { }

    public Doctor(FullName name, Email email, PhoneNumber phoneNumber,
                  Specialization specialization, CabinetNumber cabinetNumber,
                  Money consultationPrice, string? description = null)
        : base(Guid.NewGuid())
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Email = email ?? throw new ArgumentNullValueException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullValueException(nameof(phoneNumber));
        Specialization = specialization ?? throw new ArgumentNullValueException(nameof(specialization));
        CabinetNumber = cabinetNumber ?? throw new ArgumentNullValueException(nameof(cabinetNumber));
        ConsultationPrice = consultationPrice ?? throw new ArgumentNullValueException(nameof(consultationPrice));
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a schedule for the doctor.
    /// </summary>
    public void AddSchedule(DayOfWeek weekday, TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
            throw new InvalidScheduleTimeException(startTime, endTime);
        if (_schedules.Any(s => s.Weekday == weekday && s.IsActive))
            throw new ScheduleOverlapException(weekday, startTime, endTime);

        var schedule = new Schedule(this, weekday, startTime, endTime);
        _schedules.Add(schedule);
    }

    /// <summary>
    /// Checks if the doctor is available at a specific time.
    /// </summary>
    public bool IsAvailableAt(DateTime dateTime)
    {
        var schedule = _schedules.FirstOrDefault(s => s.Weekday == dateTime.DayOfWeek && s.IsActive);
        var time = TimeOnly.FromDateTime(dateTime);

        Console.WriteLine($"Checking availability: Day={dateTime.DayOfWeek}, Time={time}, Schedule found={schedule != null}");
        if (schedule != null)
        {
            Console.WriteLine($"Schedule range: {schedule.StartTime} - {schedule.EndTime}");
            Console.WriteLine($"Is within range: {time >= schedule.StartTime && time <= schedule.EndTime}");
        }

        return schedule?.ContainsTime(time) == true;
    }

    /// <summary>
    /// Adds a review from a patient.
    /// </summary>
    public Review AddReview(Patient patient, int rating, string? comment = null)
    {
        var review = new Review(this, patient, rating, comment);
        _reviews.Add(review);
        return review;
    }

    /// <summary>
    /// Updates the consultation price.
    /// </summary>
    public void UpdateConsultationPrice(Money newPrice)
    {
        ConsultationPrice = newPrice ?? throw new ArgumentNullValueException(nameof(newPrice));
    }

    /// <summary>
    /// Updates the cabinet number.
    /// </summary>
    public void UpdateCabinetNumber(CabinetNumber newCabinetNumber)
    {
        CabinetNumber = newCabinetNumber ?? throw new ArgumentNullValueException(nameof(newCabinetNumber));
    }

    /// <summary>
    /// Adds a template for conclusions.
    /// </summary>
    public Template AddTemplate(string name, string content)
    {
        var template = new Template(this, name, content);
        _templates.Add(template);
        return template;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    internal void AddAppointment(Appointment appointment)
    {
        _appointments.Add(appointment);
    }
}