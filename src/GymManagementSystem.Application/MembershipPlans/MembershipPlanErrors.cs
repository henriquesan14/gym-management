using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.MembershipPlans;

public static class MembershipPlanErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("MembershipPlan.NotFound", $"MembershipPlan with Id: {id} not found");

    public static Error Conflict(string name) =>
        Error.Conflict("MembershipPlan.Conflict", $"MembershipPlan with Name: {name} already exists");

    public static Error CreateFailure =>
        Error.Failure("MembershipPlan.CreateFailure", $"Something went wrong in creating User");

    public static Error UpdateFailure =>
        Error.Failure("MembershipPlan.UpdateFailure", $"Something went wrong in updating User");

    public static Error DeleteFailure =>
        Error.Failure("MembershipPlan.DeleteFailure", $"Something went wrong in deleting User");
}
