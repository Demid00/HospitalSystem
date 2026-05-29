using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

public class PrescribedProcedure : Entity<Guid>
{
    public Guid MedicalRecordId { get; private set; }
    public string ProcedureName { get; private set; }
    public string? Notes { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private PrescribedProcedure() { } // для EF

    internal PrescribedProcedure(Guid id, Guid medicalRecordId, string procedureName, string? notes)
        : base(id)
    {
        MedicalRecordId = medicalRecordId;
        ProcedureName = procedureName ?? throw new ArgumentNullValueException(nameof(procedureName));
        Notes = notes;
    }

    internal void Complete(DateTime completedAt)
    {
        IsCompleted = true;
        CompletedAt = completedAt;
    }
}