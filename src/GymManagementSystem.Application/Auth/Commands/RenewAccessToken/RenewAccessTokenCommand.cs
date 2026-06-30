using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Auth.Commands.RenewAccessToken;

public sealed record RenewAccessTokenCommand : ICommand<ResultT<AuthResponse>>;
