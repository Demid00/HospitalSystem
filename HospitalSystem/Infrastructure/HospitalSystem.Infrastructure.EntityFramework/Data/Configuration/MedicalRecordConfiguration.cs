using Hospital.Domain.Entities;
using Hospital.Domain.ValueObjects;
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

        builder.Property(x => x.Complaints)
            .IsRequired()
            .HasConversion(c => c.Value, str => new Complaints(str));

        builder.Property(x => x.Diagnosis)
            .IsRequired()
            .HasConversion(d => d.Value, str => new Diagnosis(str));

        builder.Property(x => x.Treatment)
            .IsRequired(false)
            .HasConversion(t => t != null ? t.Value : null, str => str != null ? new Treatment(str) : null);

        builder.Property(x => x.Conclusion)
            .IsRequired(false)
            .HasConversion(c => c != null ? c.Value : null, str => str != null ? new Conclusion(str) : null);

        builder.Property(x => x.CreatedAt)
            .HasConversion(Converters.GetUtcDateTimeConverter())
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasConversion(Converters.GetNullableUtcDateTimeConverter());

        // Настройка owned-коллекции через приватное поле
        builder.OwnsMany<PrescribedProcedure>("_prescribedProcedures", pp =>
        {
            pp.WithOwner().HasForeignKey("MedicalRecordId");
            pp.HasKey(p => p.Id);
            pp.Property(p => p.Id).ValueGeneratedNever();
            pp.Property(p => p.ProcedureName).IsRequired().HasMaxLength(200);
            pp.Property(p => p.Notes).IsRequired(false);
            pp.Property(p => p.IsCompleted).IsRequired();
            pp.Property(p => p.CompletedAt)
                .HasConversion(Converters.GetNullableUtcDateTimeConverter());
        });

        // Игнорируем публичное свойство, так как отношение уже настроено выше
        builder.Ignore(x => x.PrescribedProcedures);
    }
}