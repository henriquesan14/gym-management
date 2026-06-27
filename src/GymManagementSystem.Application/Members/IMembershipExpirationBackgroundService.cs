namespace GymManagementSystem.Application.Members;

public interface IMembershipExpirationBackgroundService
{
    Task ProcessMemberships(CancellationToken ct);
}
