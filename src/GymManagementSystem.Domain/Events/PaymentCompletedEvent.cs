using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.Payments;

namespace GymManagementSystem.Domain.Events;

public sealed record PaymentCompletedEvent(PaymentId PaymentId, MemberId MemberId, MembershipId MembershipId) : IDomainEvent;
