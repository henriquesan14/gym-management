using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Queries.GetMembers;

public record GetMembersQuery : IQuery<ResultT<IEnumerable<MemberResponse>>>;
