using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.RenewMembership;

public sealed record RenewMembershipCommand(Guid MemberId, int Months) : ICommand<Result>;
