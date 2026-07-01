namespace GymManagementSystem.Application.MembershipPlans;

public sealed record MembershipPlanResponse(Guid Id, string Name, decimal Price, int DurationInDays, bool IsActive, DateTime? CreatedAt, string? CreatedByName);
