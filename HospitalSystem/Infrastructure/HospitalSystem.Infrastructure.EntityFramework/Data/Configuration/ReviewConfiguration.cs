using Hospital.Domain.Entities;
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
        builder.Property(x => x.Rating).IsRequired();
        builder.Property(x => x.Comment).IsRequired(false);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.IsApproved).IsRequired();

        builder.HasOne(x => x.Doctor).WithMany();
        builder.HasOne(x => x.Patient).WithMany();

        builder.Navigation(x => x.Doctor).AutoInclude();
        builder.Navigation(x => x.Patient).AutoInclude();
    }
}