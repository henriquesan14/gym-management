using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.CheckIn;

public record CheckInCommand(Guid MemberId) : ICommand<Result>;