using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.CreateMember;

public record CreateMemberCommand(string FullName, string Email) : ICommand<ResultT<MemberResponse>>;
