using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Name, string Email, string Password, UserRole Role) : ICommand<ResultT<UserResponse>>;
