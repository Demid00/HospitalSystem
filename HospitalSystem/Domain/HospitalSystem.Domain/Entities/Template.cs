// Entities/Template.cs
using Hospital.Domain.Exceptions;

namespace Hospital.Domain.Entities;

public class Template : Base.Entity<Guid>
{
    public Guid DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;
    public string Name { get; private set; }
    public string Content { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Template() { }

    public Template(Doctor doctor, string name, string content)
        : this(Guid.NewGuid(), doctor, name, content) { }

    protected Template(Guid id, Doctor doctor, string name, string content)
        : base(id)
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

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public string Render(params (string placeholder, string value)[] replacements)
    {
        var result = Content;
        foreach (var (placeholder, value) in replacements)
        {
            result = result.Replace($"{{{{{placeholder}}}}}", value);
        }
        return result;
    }
}