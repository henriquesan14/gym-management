using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Enums;

namespace GymManagementSystem.Domain.Members;

public class Membership : Entity<MembershipId>
{
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public MembershipStatus Status { get; private set; }

    public bool IsActive => Status == MembershipStatus.Active;

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

    internal void Cancel()
    {
        Status = MembershipStatus.Cancelled;
    }

    internal void Expire()
    {
        Status = MembershipStatus.Expired;
    }
}
