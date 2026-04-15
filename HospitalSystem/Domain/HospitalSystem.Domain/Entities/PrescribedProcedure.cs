// Entities/PrescribedProcedure.cs
using Hospital.Domain.Exceptions;

namespace Hospital.Domain.Entities;

public class PrescribedProcedure : Base.Entity<Guid>
{
    public Guid MedicalRecordId { get; private set; }
    public MedicalRecord MedicalRecord { get; private set; } = null!;
    public Guid ProcedureId { get; private set; }
    public Procedure Procedure { get; private set; } = null!;
    public string? Notes { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private PrescribedProcedure() { }

    public PrescribedProcedure(MedicalRecord medicalRecord, Procedure procedure, string? notes = null)
        : this(Guid.NewGuid(), medicalRecord, procedure, notes) { }

    protected PrescribedProcedure(Guid id, MedicalRecord medicalRecord, Procedure procedure, string? notes)
        : base(id)
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
            throw new ProcedureAlreadyCompletedException(Id);

        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }
}