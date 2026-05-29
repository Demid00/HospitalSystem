using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

public class Review : AggregateRoot<Guid>
{
    public Guid DoctorId { get; private set; }
    public Guid PatientId { get; private set; }
    public Rating Rating { get; private set; }
    public string? Comment { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsApproved { get; private set; }

    private Review() { }

    public Review(Guid doctorId, Guid patientId, Rating rating, string? comment, DateTime createdAt)
        : base(Guid.NewGuid())
    {
        DoctorId = doctorId;
        PatientId = patientId;
        Rating = rating ?? throw new ArgumentNullValueException(nameof(rating));
        Comment = comment;
        CreatedAt = createdAt;
        IsApproved = false;
    }

    public void Approve()
    {
        IsApproved = true;
        AddDomainEvent(new ReviewApprovedEvent(Id));
    }
}

public record ReviewApprovedEvent(Guid ReviewId) : IDomainEvent;