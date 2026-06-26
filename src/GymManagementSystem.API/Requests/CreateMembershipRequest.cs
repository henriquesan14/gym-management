namespace GymManagementSystem.API.Requests;

public record CreateMembershipRequest(DateOnly StartDate, int DurationInMonths);
