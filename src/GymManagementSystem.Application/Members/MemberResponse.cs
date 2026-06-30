namespace GymManagementSystem.Application.Members;

public sealed record MemberResponse(Guid Id, string FullName, string Email, DateTime? CreatedAt, string? CreatedByName);
