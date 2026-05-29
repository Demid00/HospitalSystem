using Hospital.Domain.Entities;
using Hospital.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.EntityFramework.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.DoctorId).IsRequired();
        builder.Property(x => x.PatientId).IsRequired();

        builder.Property(x => x.Rating)
            .IsRequired()
            .HasConversion(r => r.Value, val => new Rating(val));

        builder.Property(x => x.Comment).IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .HasConversion(Converters.GetUtcDateTimeConverter());

        builder.Property(x => x.IsApproved).IsRequired();
    }
}