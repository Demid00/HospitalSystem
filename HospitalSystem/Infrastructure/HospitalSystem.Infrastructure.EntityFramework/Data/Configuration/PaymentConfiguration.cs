using Hospital.Domain.Entities;
using Hospital.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.Infrastructure.EntityFramework.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.AppointmentId).IsRequired();
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasConversion(amount => amount.Value, val => new Money(val));
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.PaidAt).IsRequired(false);
        builder.Property(x => x.TransactionId).IsRequired();

        builder.HasOne(x => x.Appointment).WithOne();
        builder.Navigation(x => x.Appointment).AutoInclude();
    }
}