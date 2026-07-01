using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.MembershipPlans.Commands.CreateMembershipPlan;

public sealed record CreateMembershipPlanCommand(string Name, decimal Price, int DurationInDays) : ICommand<ResultT<MembershipPlanResponse>>;
