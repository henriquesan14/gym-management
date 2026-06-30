using GymManagementSystem.Domain.Abstractions;

namespace GymManagementSystem.Domain.Members;

public sealed class CheckIn : Entity<CheckInId>, IAuditableEntity
{
    private CheckIn()
    {
    }
    public CheckIn(MemberId memberId)
    {
        Id = CheckInId.Of(Guid.NewGuid());
        MemberId = memberId;
        CheckedInAt = DateTime.Now;
    }

    public MemberId MemberId { get; private set; } = default!;
    public DateTime CheckedInAt { get; private set; }
}