// Entities/Doctor.cs
using Hospital.Domain.Exceptions;
using Hospital.ValueObjects;
using Hospital.Domain.Base;


namespace Hospital.Domain.Entities;

public class Doctor : Base.Entity<Guid>
{
    private readonly List<Schedule> _schedules = new();
    private readonly List<Appointment> _appointments = new();
    private readonly List<MedicalRecord> _medicalRecords = new();
    private readonly List<Template> _templates = new();
    private readonly List<Salary> _salaries = new();
    private readonly List<Review> _reviews = new();

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Specialization Specialization { get; private set; }
    public CabinetNumber CabinetNumber { get; private set; }
    public Money ConsultationPrice { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<Schedule> Schedules => _schedules.AsReadOnly();
    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public IReadOnlyCollection<MedicalRecord> MedicalRecords => _medicalRecords.AsReadOnly();
    public IReadOnlyCollection<Template> Templates => _templates.AsReadOnly();
    public IReadOnlyCollection<Salary> Salaries => _salaries.AsReadOnly();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

    private Doctor() { }

    public Doctor(User user, Specialization specialization, CabinetNumber cabinetNumber,
                  Money consultationPrice, string? description = null)
        : this(Guid.NewGuid(), user, specialization, cabinetNumber, consultationPrice, description) { }

    protected Doctor(Guid id, User user, Specialization specialization, CabinetNumber cabinetNumber,
                     Money consultationPrice, string? description)
        : base(id)
    {
        User = user ?? throw new ArgumentNullValueException(nameof(user));
        UserId = user.Id;
        Specialization = specialization ?? throw new ArgumentNullValueException(nameof(specialization));
        CabinetNumber = cabinetNumber ?? throw new ArgumentNullValueException(nameof(cabinetNumber));
        ConsultationPrice = consultationPrice ?? throw new ArgumentNullValueException(nameof(consultationPrice));
        Description = description;
        IsActive = true;
    }

    public void AddSchedule(Schedule schedule)
    {
        if (schedule.DoctorId != Id)
            throw new ScheduleDoctorMismatchException(schedule.Id, Id);

        if (_schedules.Any(s => s.Weekday == schedule.Weekday && s.IsActive))
            throw new ScheduleOverlapException(schedule.Weekday, schedule.StartTime, schedule.EndTime);

        _schedules.Add(schedule);
    }

    public void UpdatePrice(Money newPrice)
    {
        ConsultationPrice = newPrice ?? throw new ArgumentNullValueException(nameof(newPrice));
    }

    public void UpdateCabinet(CabinetNumber newCabinet)
    {
        CabinetNumber = newCabinet ?? throw new ArgumentNullValueException(nameof(newCabinet));
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new DoctorAlreadyDeactivatedException(Id);
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public bool IsAvailableAt(DateTime dateTime)
    {
        var schedule = _schedules.FirstOrDefault(s => s.Weekday == dateTime.DayOfWeek && s.IsActive);
        if (schedule == null) return false;

        var time = TimeOnly.FromDateTime(dateTime);
        return schedule.IsTimeWithinSchedule(time);
    }

    internal void AddAppointment(Appointment appointment)
    {
        _appointments.Add(appointment);
    }

    internal void AddMedicalRecord(MedicalRecord record)
    {
        _medicalRecords.Add(record);
    }

    internal void AddTemplate(Template template)
    {
        _templates.Add(template);
    }

    internal void AddSalary(Salary salary)
    {
        _salaries.Add(salary);
    }

    internal void AddReview(Review review)
    {
        _reviews.Add(review);
    }
}