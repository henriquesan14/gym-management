using GymManagementSystem.Application.Members;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Infra.Data;
using GymManagementSystem.Infra.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GymManagementSystem.Infra.Services;

public sealed class MembershipExpirationBackgroundService(ILogger<MembershipExpirationBackgroundService> logger, GymManagementDbContext dbContext, IOptions<MembershipOptions> options)
    : IMembershipExpirationBackgroundService
{
    private readonly MembershipOptions _options = options.Value;
    public async Task ProcessMemberships(CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

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
                     daysExpired >= _options.GracePeriodDays)
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
