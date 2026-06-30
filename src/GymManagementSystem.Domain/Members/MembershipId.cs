using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.Members;

public sealed record MembershipId(Guid Value)
{
    public static MembershipId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("MembershipId cannot be empty.");

        return new(value);
    }

    public static MembershipId New() =>
        new(Guid.NewGuid());
}
