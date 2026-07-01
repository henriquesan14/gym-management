using GymManagementSystem.Domain.Abstractions;

namespace GymManagementSystem.Domain.MembershipPlans;

public sealed class MembershipPlan : Aggregate<MembershipPlanId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int DurationInDays { get; private set; }
    public bool IsActive { get; private set; }

    private MembershipPlan() { }

    public static MembershipPlan Create(
        MembershipPlanId id,
        string name,
        decimal price,
        int durationInDays)
    {
        return new MembershipPlan
        {
            Id = id,
            Name = name,
            Price = price,
            DurationInDays = durationInDays,
            IsActive = true
        };
    }
}
