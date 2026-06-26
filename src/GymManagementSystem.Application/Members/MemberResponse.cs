namespace GymManagementSystem.Application.Members;

public record MemberResponse(Guid Id, string FullName, string Email, DateTime? CreatedAt, string? CreatedByName);
