namespace GymManagementSystem.Infra.Options;

public sealed class MembershipOptions
{
    public const string SectionName = "Membership";

    public int GracePeriodDays { get; init; }
}
