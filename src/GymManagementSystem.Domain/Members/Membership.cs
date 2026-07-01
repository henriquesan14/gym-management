using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Domain.MembershipPlans;

namespace GymManagementSystem.Domain.Members;

public sealed class Membership : Entity<MembershipId>, IAuditableEntity
{
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public MembershipStatus Status { get; private set; }
    public MembershipPlanId MembershipPlanId { get; private set; } = default!;

    public bool IsActive => Status == MembershipStatus.Active && EndDate >= DateOnly.FromDateTime(DateTime.Now);

    private Membership()
    {
    }

    internal Membership(
        MembershipId id,
        MembershipPlanId membershipPlanId,
        DateOnly startDate,
        DateOnly endDate)
    {
        Id = id;
        MembershipPlanId = membershipPlanId;
        StartDate = startDate;
        EndDate = endDate;
        Status = MembershipStatus.Active;
    }

    public void EnterGracePeriod()
    {
        if (Status != MembershipStatus.Active)
            return;

        Status = MembershipStatus.GracePeriod;
    }

    public void Cancel()
    {
        if (Status == MembershipStatus.Cancelled)
            return;
        Status = MembershipStatus.Cancelled;
    }

    public void Expire()
    {
        if (Status != MembershipStatus.GracePeriod)
            return;
        Status = MembershipStatus.Expired;
    }

    public void Renew(MembershipPlan plan)
    {
        MembershipPlanId = plan.Id;
        EndDate = EndDate.AddDays(plan.DurationInDays);

        if (Status != MembershipStatus.Cancelled)
            Status = MembershipStatus.Active;
    }
}
