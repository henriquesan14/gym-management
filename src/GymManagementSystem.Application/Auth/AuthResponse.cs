using GymManagementSystem.Domain.Enums;

namespace GymManagementSystem.Application.Auth;

public record AuthResponse(Guid UserId, string Name, UserRole Role);
