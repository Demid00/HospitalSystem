// Entities/MedicalRecord.cs
using Hospital.Domain.Exceptions;
using Hospital.Domain.Base;

namespace Hospital.Domain.Entities;

public class MedicalRecord : Base.Entity<Guid>
{
    private readonly List<PrescribedProcedure> _prescribedProcedures = new();

    public Guid AppointmentId { get; private set; }
    public Appointment Appointment { get; private set; } = null!;
    public string Complaints { get; private set; }
    public string Diagnosis { get; private set; }
    public string? Treatment { get; private set; }
    public string? Conclusion { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    public IReadOnlyCollection<PrescribedProcedure> PrescribedProcedures => _prescribedProcedures.AsReadOnly();

    private MedicalRecord() { }

    public MedicalRecord(Appointment appointment, string complaints, string diagnosis,
                         string? treatment = null, string? conclusion = null)
        : this(Guid.NewGuid(), appointment, complaints, diagnosis, treatment, conclusion) { }

    protected MedicalRecord(Guid id, Appointment appointment, string complaints, string diagnosis,
                            string? treatment, string? conclusion)
        : base(id)
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

    public void AddPrescribedProcedure(Procedure procedure, string? notes = null)
    {
        var prescribed = new PrescribedProcedure(this, procedure, notes);
        _prescribedProcedures.Add(prescribed);
    }
}