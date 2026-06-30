using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.ValueObjects;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.CreateMember;

public sealed class CreateMemberCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateMemberCommand, ResultT<MemberResponse>>
{
    public async Task<ResultT<MemberResponse>> Handle(CreateMemberCommand request, CancellationToken ct)
    {
        var emailExist = await unitOfWork.Members.EmailExistsAsync(request.Email, ct);

        if (emailExist) return MemberErrors.Conflict(request.Email);

        var member = new Member(MemberId.New(), request.FullName, Email.Of(request.Email));

        await unitOfWork.Members.AddAsync(member, ct);
        await unitOfWork.CompleteAsync(ct);

        return member.ToDto();
    }
}
