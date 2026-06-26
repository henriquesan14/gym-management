using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.Members;

public record MembershipId
{
    public Guid Value { get; }

    private MembershipId(Guid value) => Value = value;

    public static MembershipId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("MembershipId cannot be empty.");
        }
        return new MembershipId(value);
    }
}
