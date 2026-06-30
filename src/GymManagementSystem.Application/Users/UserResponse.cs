using GymManagementSystem.Domain.Enums;

namespace GymManagementSystem.Application.Users;

public sealed record UserResponse(Guid Id, string Name, string Email, UserRole Role, DateTime? CreatedAt, string? CreatedByName);
