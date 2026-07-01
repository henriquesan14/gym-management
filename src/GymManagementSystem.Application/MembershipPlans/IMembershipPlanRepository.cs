using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.MembershipPlans;

namespace GymManagementSystem.Application.MembershipPlans;

public interface IMembershipPlanRepository : IRepository<MembershipPlan, MembershipPlanId>
{
    Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default);
}
