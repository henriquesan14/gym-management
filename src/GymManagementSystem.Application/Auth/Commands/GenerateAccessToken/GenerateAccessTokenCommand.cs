using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Auth.Commands.GenerateAccessToken;

public sealed record GenerateAccessTokenCommand(string Email, string Password) : ICommand<ResultT<AuthResponse>>;
