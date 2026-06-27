using GymManagementSystem.Domain.Enums;

namespace GymManagementSystem.Application.Members;

public record MembershipResponse(Guid Id, DateOnly StartDate, DateOnly EndDate, MembershipStatus Status, 
    DateTime? CreatedAt, string? CreatedByName);

