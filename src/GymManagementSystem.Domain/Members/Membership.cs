using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.Members;

public class Membership : Entity<MembershipId>, IAuditableEntity
{
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public MembershipStatus Status { get; private set; }

    public bool IsActive => Status == MembershipStatus.Active && EndDate >= DateOnly.FromDateTime(DateTime.Now);

    private Membership()
    {
    }

    internal Membership(
        MembershipId id,
        DateOnly startDate,
        DateOnly endDate)
    {
        Id = id;
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

    public void Renew(int months)
    {
        if (Status == MembershipStatus.Cancelled || Status == MembershipStatus.Expired)
            throw new DomainException("Cannot renew a cancelled/expired membership.");

        EndDate = EndDate.AddMonths(months);
    }
}
