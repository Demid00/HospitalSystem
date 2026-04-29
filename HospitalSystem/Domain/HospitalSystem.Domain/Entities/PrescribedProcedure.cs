using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents a procedure prescribed to a patient.
/// </summary>
public class PrescribedProcedure : Entity<Guid>
{
    public Guid MedicalRecordId { get; }
    public MedicalRecord MedicalRecord { get; private set; } = null!;
    public Guid ProcedureId { get; }
    public Procedure Procedure { get; private set; } = null!;
    public string? Notes { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private PrescribedProcedure() { }

    internal PrescribedProcedure(MedicalRecord medicalRecord, Procedure procedure, string? notes = null)
        : base(Guid.NewGuid())
    {
        MedicalRecord = medicalRecord ?? throw new ArgumentNullValueException(nameof(medicalRecord));
        MedicalRecordId = medicalRecord.Id;
        Procedure = procedure ?? throw new ArgumentNullValueException(nameof(procedure));
        ProcedureId = procedure.Id;
        Notes = notes;
        IsCompleted = false;
    }

    public void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException($"Procedure {Id} is already completed");

        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }
}