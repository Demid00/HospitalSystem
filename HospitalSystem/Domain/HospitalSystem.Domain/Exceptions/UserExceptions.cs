// Exceptions/UserExceptions.cs
namespace Hospital.Domain.Exceptions;

public class UserAlreadyDeactivatedException : DomainException
{
    public Guid UserId { get; }

    public UserAlreadyDeactivatedException(Guid userId)
        : base($"User {userId} is already deactivated.") => UserId = userId;
}

public class UserNotFoundException : DomainException
{
    public Guid UserId { get; }

    public UserNotFoundException(Guid userId)
        : base($"User with ID {userId} was not found.") => UserId = userId;
}

public class UserAlreadyExistsException : DomainException
{
    public string Email { get; }

    public UserAlreadyExistsException(string email)
        : base($"User with email {email} already exists.") => Email = email;
}