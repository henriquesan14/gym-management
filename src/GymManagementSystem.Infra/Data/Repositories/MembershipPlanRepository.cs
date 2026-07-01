using GymManagementSystem.Application.MembershipPlans;
using GymManagementSystem.Domain.MembershipPlans;
using GymManagementSystem.Domain.MembershipPlans.Specifications;

namespace GymManagementSystem.Infra.Data.Repositories;

public sealed class MembershipPlanRepository : Repository<MembershipPlan, MembershipPlanId>, IMembershipPlanRepository
{
    public MembershipPlanRepository(GymManagementDbContext db) : base(db)
    {
    }

    public Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default)
        => AnyAsync(new MembershipPlanNameSpecification(name), cancellationToken);
}
