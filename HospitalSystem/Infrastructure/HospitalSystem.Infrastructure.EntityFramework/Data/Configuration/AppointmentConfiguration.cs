using Hospital.Domain.Entities;
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

        builder.Property(x => x.DateTime).IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Price)
            .IsRequired()
            .HasConversion(price => price.Value, val => new Money(val));

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CompletedAt).IsRequired(false);
        builder.Property(x => x.CancelledAt).IsRequired(false);
        builder.Property(x => x.CancellationReason).IsRequired(false);

        builder.HasOne(x => x.Patient).WithMany();
        builder.HasOne(x => x.Doctor).WithMany();
        builder.HasOne(x => x.Payment).WithOne().HasForeignKey<Payment>("AppointmentId");
        builder.HasOne(x => x.MedicalRecord).WithOne().HasForeignKey<MedicalRecord>("AppointmentId");

        builder.Navigation(x => x.Patient).AutoInclude();
        builder.Navigation(x => x.Doctor).AutoInclude();
    }
}