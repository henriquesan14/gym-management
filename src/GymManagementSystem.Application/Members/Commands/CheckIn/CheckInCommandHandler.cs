using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.Members.Specifications;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.CheckIn;

public class CheckInCommandHandler(
    IUnitOfWork unitOfWork, ICheckInService checkInService)
    : ICommandHandler<CheckInCommand, Result>
{
    public async Task<Result> Handle(
        CheckInCommand request,
        CancellationToken ct)
    {
        var member = await unitOfWork.Members
            .SingleOrDefaultAsync(
                new MemberByIdWithMembershipsSpecification(
                    request.MemberId));

        if (member is null)
            return MemberErrors.NotFound(request.MemberId);

        var today = DateOnly.FromDateTime(DateTime.Today);
        var alreadyCheckedInToday = await checkInService.HasCheckedInTodayAsync(MemberId.Of(request.MemberId), today, new CancellationToken());

        if (alreadyCheckedInToday)
            return MemberErrors.AlreadyCheckedInToday();

        member.CheckIn();

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
