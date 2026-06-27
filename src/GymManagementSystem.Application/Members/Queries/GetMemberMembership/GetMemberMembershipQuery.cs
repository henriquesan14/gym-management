using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Queries.GetMemberMembership;

public record GetMemberMembershipQuery(Guid MemberId)
    : IQuery<ResultT<IEnumerable<MembershipResponse>>>;
