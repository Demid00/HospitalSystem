using Hospital.Domain.Entities;
using Hospital.Domain.ValueObjects;
using Hospital.Domain.ValueObjects.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.EntityFramework.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(name => name.Value, str => new FullName(str))
            .HasMaxLength(FullNameValidator.MAX_LENGTH);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasConversion(email => email.Value, str => new Email(str));

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasConversion(phone => phone.Value, str => new PhoneNumber(str));

        builder.Property(x => x.BirthDate).IsRequired();
        builder.Property(x => x.InsurancePolicy).IsRequired(false);
        builder.Property(x => x.Allergies).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasMany<Appointment>("_appointments")
            .WithOne()
            .HasForeignKey("PatientId")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.Appointments);
        builder.Ignore(x => x.HasActiveAppointments);
        builder.Ignore(x => x.TotalSpent);
    }
}