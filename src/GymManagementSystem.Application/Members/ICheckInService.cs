using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Application.Members;

public interface ICheckInService
{
    public Task<bool> HasCheckedInTodayAsync(MemberId memberId,
        DateOnly today,
        CancellationToken ct);
}
