using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.CreateMembership;

public sealed record CreateMembershipCommand(Guid MemberId, DateOnly StartDate, int DurationInMonths) : ICommand<Result>;
