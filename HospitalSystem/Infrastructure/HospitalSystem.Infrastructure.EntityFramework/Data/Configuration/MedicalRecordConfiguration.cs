using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.EntityFramework.Data.Configurations;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.AppointmentId).IsRequired();
        builder.Property(x => x.Complaints).IsRequired();
        builder.Property(x => x.Diagnosis).IsRequired();
        builder.Property(x => x.Treatment).IsRequired(false);
        builder.Property(x => x.Conclusion).IsRequired(false);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);

        builder.HasMany<PrescribedProcedure>("_prescribedProcedures")
            .WithOne()
            .HasForeignKey("MedicalRecordId")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.PrescribedProcedures);
    }
}