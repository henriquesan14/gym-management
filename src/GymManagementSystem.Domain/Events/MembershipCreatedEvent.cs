using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Domain.Events;

public sealed record MembershipCreatedEvent(MemberId memberId, MembershipId membershipId) : IDomainEvent;
