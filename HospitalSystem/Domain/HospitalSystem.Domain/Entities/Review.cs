// Entities/Review.cs
using Hospital.Domain.Exceptions;

namespace Hospital.Domain.Entities;

public class Review : Base.Entity<Guid>
{
    public Guid PatientId { get; private set; }
    public Patient Patient { get; private set; } = null!;
    public Guid DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;
    public int Rating { get; private set; }
    public string? Comment { get; private set; }
    public DateTime CreatedAt { get; }
    public bool IsApproved { get; private set; }
    public DateTime? ApprovedAt { get; private set; }

    private Review() { }

    public Review(Patient patient, Doctor doctor, int rating, string? comment = null)
        : this(Guid.NewGuid(), patient, doctor, rating, comment) { }

    protected Review(Guid id, Patient patient, Doctor doctor, int rating, string? comment)
        : base(id)
    {
        Patient = patient ?? throw new ArgumentNullValueException(nameof(patient));
        PatientId = patient.Id;
        Doctor = doctor ?? throw new ArgumentNullValueException(nameof(doctor));
        DoctorId = doctor.Id;

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
        ApprovedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        IsApproved = false;
        ApprovedAt = null;
    }
}