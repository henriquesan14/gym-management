using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Domain.Events;

public record MembershipCreatedEvent(MemberId memberId, MembershipId membershipId) : IDomainEvent;
