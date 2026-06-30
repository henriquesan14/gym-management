using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.CancelMembership;

public sealed record CancelMembershipCommand(Guid MemberId, Guid MembershipId) : ICommand<Result>;
