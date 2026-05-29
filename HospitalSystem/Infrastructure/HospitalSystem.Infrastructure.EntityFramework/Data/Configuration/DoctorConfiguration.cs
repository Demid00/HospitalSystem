using Hospital.Domain.Entities;
using Hospital.Domain.ValueObjects;
using Hospital.Domain.ValueObjects.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.EntityFramework.Data.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(name => name.Value, str => new FullName(str))
            .HasMaxLength(FullNameValidator.MaxLength);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasConversion(email => email.Value, str => new Email(str));

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasConversion(phone => phone.Value, str => new PhoneNumber(str));

        builder.Property(x => x.Specialization)
            .IsRequired()
            .HasConversion(spec => spec.Value, str => new Specialization(str))
            .HasMaxLength(SpecializationValidator.MaxLength);

        builder.Property(x => x.CabinetNumber)
            .IsRequired()
            .HasConversion(cab => cab.Value, val => new CabinetNumber(val));

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasConversion(
                desc => desc != null ? desc.Value : null,
                str => str != null ? new Description(str) : null
            );

        builder.Property(x => x.IsActive).IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasConversion(Converters.GetUtcDateTimeConverter());
    }
}