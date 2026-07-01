using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.MembershipPlans;

public sealed record MembershipPlanId(Guid Value)
{
    public static MembershipPlanId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("MembershipPlanId cannot be empty.");

        return new(value);
    }

    public static MembershipPlanId New() =>
        new(Guid.NewGuid());
}
