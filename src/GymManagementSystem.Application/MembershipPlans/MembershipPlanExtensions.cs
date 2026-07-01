using GymManagementSystem.Domain.MembershipPlans;

namespace GymManagementSystem.Application.MembershipPlans;

public static class MembershipPlanExtensions
{
    public static MembershipPlanResponse ToDto(this MembershipPlan membershipPlan)
    {
        return new MembershipPlanResponse(
            membershipPlan.Id.Value,
            membershipPlan.Name,
            membershipPlan.Price,
            membershipPlan.DurationInDays,
            membershipPlan.IsActive,
            membershipPlan.CreatedAt,
            membershipPlan.CreatedByName
        );
    }

    public static List<MembershipPlanResponse> ToDto(this IEnumerable<MembershipPlan> membershipPlans)
    {
        return membershipPlans
            .Select(ToDto)
            .ToList();
    }
}
