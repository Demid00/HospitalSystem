using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Hospital.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.EntityFramework.Data.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.PatientId).IsRequired();
        builder.Property(x => x.DoctorId).IsRequired();

        // Исправленный конвертер: сначала преобразуем AppointmentDateTime в DateTime (с нормализацией UTC),
        // затем из БД читаем DateTime и создаём AppointmentDateTime (тоже с нормализацией)
        builder.Property(x => x.DateTime)
            .HasConversion(
                dt => dt.Value.Kind == DateTimeKind.Utc ? dt.Value : DateTime.SpecifyKind(dt.Value, DateTimeKind.Utc),
                val => new AppointmentDateTime(DateTime.SpecifyKind(val, DateTimeKind.Utc))
            );

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.CreatedAt)
            .HasConversion(Converters.GetUtcDateTimeConverter());

        builder.Property(x => x.CompletedAt)
            .HasConversion(Converters.GetNullableUtcDateTimeConverter());

        builder.Property(x => x.CancelledAt)
            .HasConversion(Converters.GetNullableUtcDateTimeConverter());

        builder.Property(x => x.CancellationReason)
            .IsRequired(false)
            .HasConversion(r => r != null ? r.Value : null, str => str != null ? new CancellationReason(str) : null);
    }
}