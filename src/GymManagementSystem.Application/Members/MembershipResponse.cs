using GymManagementSystem.Domain.Enums;

namespace GymManagementSystem.Application.Members;

public sealed record MembershipResponse(Guid Id, DateOnly StartDate, DateOnly EndDate, MembershipStatus Status, 
    DateTime? CreatedAt, string? CreatedByName);

