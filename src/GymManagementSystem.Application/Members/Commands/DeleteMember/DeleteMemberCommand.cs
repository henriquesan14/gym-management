using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.DeleteMember;

public record DeleteMemberCommand(Guid Id) : ICommand<Result>;
