using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents a medical record for an appointment.
/// </summary>
public class MedicalRecord : Entity<Guid>
{
    private readonly List<PrescribedProcedure> _prescribedProcedures = [];

    public Guid AppointmentId { get; }
    public Appointment Appointment { get; private set; } = null!;
    public string Complaints { get; private set; }
    public string Diagnosis { get; private set; }
    public string? Treatment { get; private set; }
    public string? Conclusion { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    public IReadOnlyCollection<PrescribedProcedure> PrescribedProcedures => _prescribedProcedures.AsReadOnly();

    private MedicalRecord() { }

    internal MedicalRecord(Appointment appointment, string complaints, string diagnosis,
                           string? treatment = null, string? conclusion = null)
        : base(Guid.NewGuid())
    {
        Appointment = appointment ?? throw new ArgumentNullValueException(nameof(appointment));
        AppointmentId = appointment.Id;
        Complaints = complaints ?? throw new ArgumentNullValueException(nameof(complaints));
        Diagnosis = diagnosis ?? throw new ArgumentNullValueException(nameof(diagnosis));
        Treatment = treatment;
        Conclusion = conclusion;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? complaints, string? diagnosis, string? treatment, string? conclusion)
    {
        if (!string.IsNullOrWhiteSpace(complaints))
            Complaints = complaints;
        if (!string.IsNullOrWhiteSpace(diagnosis))
            Diagnosis = diagnosis;
        if (!string.IsNullOrWhiteSpace(treatment))
            Treatment = treatment;
        if (!string.IsNullOrWhiteSpace(conclusion))
            Conclusion = conclusion;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddProcedure(Procedure procedure, string? notes = null)
    {
        var prescribed = new PrescribedProcedure(this, procedure, notes);
        _prescribedProcedures.Add(prescribed);
    }
}