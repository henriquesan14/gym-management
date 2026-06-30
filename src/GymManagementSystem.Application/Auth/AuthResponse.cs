using GymManagementSystem.Domain.Enums;

namespace GymManagementSystem.Application.Auth;

public sealed record AuthResponse(Guid UserId, string Name, UserRole Role);
