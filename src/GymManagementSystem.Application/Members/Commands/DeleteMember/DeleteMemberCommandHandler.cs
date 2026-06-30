using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.DeleteMember;

public sealed class DeleteMemberCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteMemberCommand, Result>
{
    public async Task<Result> Handle(DeleteMemberCommand request, CancellationToken ct)
    {
        var member = await unitOfWork.Members.GetByIdAsync(MemberId.Of(request.Id), ct);
        if (member == null) return MemberErrors.NotFound(request.Id);

        await unitOfWork.Members.DeleteAsync(member, ct);

        return Result.Success();
    }
}
