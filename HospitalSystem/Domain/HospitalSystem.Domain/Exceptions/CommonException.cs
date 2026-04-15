// Exceptions/CommonExceptions.cs
namespace Hospital.Domain.Exceptions;

public class ArgumentNullValueException : ArgumentNullException
{
    public ArgumentNullValueException(string paramName)
        : base(paramName, $"Argument \"{paramName}\" value is null") { }
}

public class InvalidOperationForEntityException : DomainException
{
    public Guid EntityId { get; }
    public string EntityType { get; }

    public InvalidOperationForEntityException(string entityType, Guid entityId, string operation)
        : base($"Cannot perform '{operation}' on {entityType} with ID {entityId}.")
    {
        EntityType = entityType;
        EntityId = entityId;
    }
}