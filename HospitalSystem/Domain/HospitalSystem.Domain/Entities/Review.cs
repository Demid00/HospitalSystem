using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents a review of a doctor by a patient.
/// </summary>
public class Review : Entity<Guid>
{
    public Guid DoctorId { get; }
    public Doctor Doctor { get; private set; } = null!;
    public Guid PatientId { get; }
    public Patient Patient { get; private set; } = null!;
    public int Rating { get; }
    public string? Comment { get; }
    public DateTime CreatedAt { get; }
    public bool IsApproved { get; private set; }

    private Review() { }

    internal Review(Doctor doctor, Patient patient, int rating, string? comment = null)
        : base(Guid.NewGuid())
    {
        Doctor = doctor ?? throw new ArgumentNullValueException(nameof(doctor));
        DoctorId = doctor.Id;
        Patient = patient ?? throw new ArgumentNullValueException(nameof(patient));
        PatientId = patient.Id;

        if (rating < 1 || rating > 5)
            throw new InvalidRatingException(rating);

        Rating = rating;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
        IsApproved = false;
    }

    public void Approve()
    {
        IsApproved = true;
    }
}