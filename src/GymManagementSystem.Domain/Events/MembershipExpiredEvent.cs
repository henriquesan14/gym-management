using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Domain.Events;

public sealed record MembershipExpiredEvent(MemberId MemberId, MembershipId MembershipId) : IDomainEvent;

