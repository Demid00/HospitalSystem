using Hospital.Domain.Entities;
using Hospital.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.EntityFramework.Data.Configurations;

public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
{
    public void Configure(EntityTypeBuilder<Procedure> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired(false);
        builder.Property(x => x.Price)
            .IsRequired()
            .HasConversion(price => price.Value, val => new Money(val));
        builder.Property(x => x.EstimatedDurationMinutes).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
    }
}