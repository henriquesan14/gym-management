using Ardalis.Specification;

namespace GymManagementSystem.Domain.MembershipPlans.Specifications;

public sealed class MembershipPlanNameSpecification : SingleResultSpecification<MembershipPlan>
{
    public MembershipPlanNameSpecification(string name)
    {
        var normalized = name.Trim().ToLowerInvariant();

        Query
            .Where(m => m.Name == normalized);
    }
}
