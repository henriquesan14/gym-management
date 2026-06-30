namespace GymManagementSystem.API.Requests;

public sealed record CreateMembershipRequest(DateOnly StartDate, int DurationInMonths);
