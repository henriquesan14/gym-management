using GymManagementSystem.Application.Members;
using GymManagementSystem.Domain.Members;
using GymManagementSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infra.Services;

public class CheckInService(GymManagementDbContext dbContext) : ICheckInService
{
    public async Task<bool> HasCheckedInTodayAsync(
        MemberId memberId,
        DateOnly today,
        CancellationToken ct)
    {
        return await dbContext.CheckIns.AnyAsync(
            x => x.MemberId == memberId &&
                 DateOnly.FromDateTime(x.CheckedInAt) == today,
            ct);
    }
}
