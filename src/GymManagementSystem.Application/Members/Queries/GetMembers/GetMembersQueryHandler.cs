using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Queries.GetMembers;

public class GetMembersQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetMembersQuery, ResultT<IEnumerable<MemberResponse>>>
{
    public async Task<ResultT<IEnumerable<MemberResponse>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await unitOfWork.Members.ListAsync();
        return members.ToDto();
    }
}
