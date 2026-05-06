using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.EntityFramework.Data.Configurations;

public class PrescribedProcedureConfiguration : IEntityTypeConfiguration<PrescribedProcedure>
{
    public void Configure(EntityTypeBuilder<PrescribedProcedure> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.MedicalRecordId).IsRequired();
        builder.Property(x => x.ProcedureId).IsRequired();
        builder.Property(x => x.Notes).IsRequired(false);
        builder.Property(x => x.IsCompleted).IsRequired();
        builder.Property(x => x.CompletedAt).IsRequired(false);

        builder.HasOne(x => x.MedicalRecord).WithMany();
        builder.HasOne(x => x.Procedure).WithMany();

        builder.Navigation(x => x.MedicalRecord).AutoInclude();
        builder.Navigation(x => x.Procedure).AutoInclude();
    }
}