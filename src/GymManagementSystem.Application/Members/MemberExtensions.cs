using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Application.Members;

public static class MemberExtensions
{
    public static MemberResponse ToDto(this Member member)
    {
        return new MemberResponse(
            member.Id.Value,
            member.FullName,
            member.Email.Value,
            member.CreatedAt,
            member.CreatedByName
        );
    }

    public static List<MemberResponse> ToDto(this IEnumerable<Member> members)
    {
        return members
            .Select(ToDto)
            .ToList();
    }
}
