using GymManagementSystem.Application.Members;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManagementSystem.Infra.Services;

public sealed class MembershipExpirationBackgroundService(ILogger<MembershipExpirationBackgroundService> logger, GymManagementDbContext dbContext)
    : IMembershipExpirationBackgroundService
{
    public async Task ProcessMemberships(CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var memberships = await dbContext.Memberships
            .Where(x =>
                x.Status == MembershipStatus.Active &&
                x.EndDate <= today)
            .ToListAsync(ct);

        foreach (var membership in memberships)
        {
            membership.Expire();
        }

        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation(
            "{Count} memberships expired.",
            memberships.Count);
    }
}
