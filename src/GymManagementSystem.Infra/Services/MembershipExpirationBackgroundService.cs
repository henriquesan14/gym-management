using GymManagementSystem.Application.Members;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GymManagementSystem.Infra.Services;

public sealed class MembershipExpirationBackgroundService(ILogger<MembershipExpirationBackgroundService> logger, GymManagementDbContext dbContext, IConfiguration configuration)
    : IMembershipExpirationBackgroundService
{
    public async Task ProcessMemberships(CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var gracePeriodDays = Int32.Parse(configuration["GracePeriodDays"]!);

        var memberships = await dbContext.Memberships
            .Where(x =>
                x.Status == MembershipStatus.Active ||
                x.Status == MembershipStatus.GracePeriod)
            .ToListAsync(ct);

        foreach (var membership in memberships)
        {
            var daysExpired = today.DayNumber - membership.EndDate.DayNumber;

            if (membership.Status == MembershipStatus.Active &&
                daysExpired >= 0)
            {
                membership.EnterGracePeriod();
            }
            else if (membership.Status == MembershipStatus.GracePeriod &&
                     daysExpired >= gracePeriodDays)
            {
                membership.Expire();
            }
        }

        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation(
            "{Count} memberships processed.",
            memberships.Count);
    }
}
