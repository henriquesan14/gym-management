using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members.Specifications;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Queries.GetMemberMembership;

public class GetMemberMembershipQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetMemberMembershipQuery, ResultT<IEnumerable<MembershipResponse>>>
{
    public async Task<ResultT<IEnumerable<MembershipResponse>>> Handle(GetMemberMembershipQuery request, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.Members.SingleOrDefaultAsync(new MemberByIdWithMembershipsSpecification(request.MemberId));

        if (member is null)
            return new List<MembershipResponse>();

        return member.Memberships
            .Select(x => new MembershipResponse(
                x.Id.Value,
                x.StartDate,
                x.EndDate,
                x.Status,
                x.CreatedAt,
                x.CreatedByName))
            .ToList();
    }
}
