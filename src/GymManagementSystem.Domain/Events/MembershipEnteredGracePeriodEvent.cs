using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Domain.Events;

public sealed record MembershipEnteredGracePeriodEvent(MemberId Id, MembershipId MembershipId) : IDomainEvent;
