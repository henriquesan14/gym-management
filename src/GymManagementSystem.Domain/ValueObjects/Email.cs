using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.ValueObjects;

public record Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Email Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Email cannot be empty.");
        }
        if (!value.Contains('@'))
        {
            throw new DomainException("Email is not valid.");
        }
        return new Email(value.Trim().ToLowerInvariant());
    }

    public override string ToString() => Value;
}
