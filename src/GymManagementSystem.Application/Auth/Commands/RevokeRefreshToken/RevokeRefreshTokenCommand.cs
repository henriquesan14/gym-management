using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Auth.Commands.RevokeRefreshToken;

public record RevokeRefreshTokenCommand : ICommand<Result>;