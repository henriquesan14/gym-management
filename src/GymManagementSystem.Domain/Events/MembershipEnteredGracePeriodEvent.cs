using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Domain.Events;

public record MembershipEnteredGracePeriodEvent(MemberId Id, MembershipId MembershipId) : IDomainEvent;
