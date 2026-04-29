using Hospital.Domain.Base;
using Hospital.Domain.Exceptions;
using Hospital.Domain.ValueObjects;

namespace Hospital.Domain.Entities;

/// <summary>
/// Represents a template for medical conclusions.
/// </summary>
public class Template : Entity<Guid>
{
    public Guid DoctorId { get; }
    public Doctor Doctor { get; private set; } = null!;
    public string Name { get; private set; }
    public string Content { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Template() { }

    internal Template(Doctor doctor, string name, string content)
        : base(Guid.NewGuid())
    {
        Doctor = doctor ?? throw new ArgumentNullValueException(nameof(doctor));
        DoctorId = doctor.Id;
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Content = content ?? throw new ArgumentNullValueException(nameof(content));
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string content)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Content = content ?? throw new ArgumentNullValueException(nameof(content));
        UpdatedAt = DateTime.UtcNow;
    }

    public string Render(Dictionary<string, string> replacements)
    {
        var result = Content;
        foreach (var (key, value) in replacements)
        {
            result = result.Replace($"{{{{{key}}}}}", value);
        }
        return result;
    }

    public void Deactivate() => IsActive = false;
}