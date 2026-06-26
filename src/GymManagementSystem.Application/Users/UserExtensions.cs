using GymManagementSystem.Domain.Users;

namespace GymManagementSystem.Application.Users;

public static class UserExtensions
{
    public static UserResponse ToDto(this User user)
    {
        return new UserResponse(
            user.Id.Value,
            user.Name,
            user.Email.Value,
            user.Role,
            user.CreatedAt,
            user.CreatedByName
        );
    }

    public static List<UserResponse> ToDto(this IEnumerable<User> users)
    {
        return users
            .Select(ToDto)
            .ToList();
    }
}
