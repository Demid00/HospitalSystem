using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HospitalSystem.Infrastructure.EntityFramework.Data;

public static class Converters
{
    // Для не-nullable DateTime
    public static ValueConverter<DateTime, DateTime> GetUtcDateTimeConverter() => new(
        v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
    );

    // Для nullable DateTime
    public static ValueConverter<DateTime?, DateTime?> GetNullableUtcDateTimeConverter() => new(
        v => v.HasValue ? (v.Value.Kind == DateTimeKind.Utc ? v.Value : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)) : null,
        v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null
    );
}