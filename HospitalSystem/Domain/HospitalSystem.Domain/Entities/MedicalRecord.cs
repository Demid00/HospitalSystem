using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

public class MedicalRecord : AggregateRoot<Guid>
{
    public Guid AppointmentId { get; private set; }
    public Complaints Complaints { get; private set; }
    public Diagnosis Diagnosis { get; private set; }
    public Treatment? Treatment { get; private set; }
    public Conclusion? Conclusion { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<PrescribedProcedure> _prescribedProcedures = new();
    public IReadOnlyCollection<PrescribedProcedure> PrescribedProcedures => _prescribedProcedures.AsReadOnly();

    private MedicalRecord() { } // для EF

    public MedicalRecord(Guid appointmentId, Complaints complaints, Diagnosis diagnosis,
                         Treatment? treatment, Conclusion? conclusion, DateTime createdAt)
        : base(Guid.NewGuid())
    {
        AppointmentId = appointmentId;
        Complaints = complaints ?? throw new ArgumentNullValueException(nameof(complaints));
        Diagnosis = diagnosis ?? throw new ArgumentNullValueException(nameof(diagnosis));
        Treatment = treatment;
        Conclusion = conclusion;
        CreatedAt = createdAt;
    }

    public void AddPrescribedProcedure(string procedureName, string? notes, DateTime now)
    {
        var procedure = new PrescribedProcedure(Guid.NewGuid(), Id, procedureName, notes);
        _prescribedProcedures.Add(procedure);
        UpdatedAt = now;
        AddDomainEvent(new ProcedurePrescribedEvent(Id, procedureName));
    }

    public void CompleteProcedure(Guid prescribedProcedureId, DateTime completedAt)
    {
        var proc = _prescribedProcedures.FirstOrDefault(p => p.Id == prescribedProcedureId)
            ?? throw new InvalidOperationException($"Procedure {prescribedProcedureId} not found.");
        proc.Complete(completedAt);
        UpdatedAt = completedAt;
    }
}

public record ProcedurePrescribedEvent(Guid MedicalRecordId, string ProcedureName) : IDomainEvent;