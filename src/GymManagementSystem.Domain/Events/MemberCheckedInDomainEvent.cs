using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Domain.Events;

public sealed record MemberCheckedInDomainEvent(MemberId MemberId) : IDomainEvent;
