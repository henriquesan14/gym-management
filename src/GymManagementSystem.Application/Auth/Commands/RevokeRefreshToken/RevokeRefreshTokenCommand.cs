using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Auth.Commands.RevokeRefreshToken;

public sealed record RevokeRefreshTokenCommand : ICommand<Result>;