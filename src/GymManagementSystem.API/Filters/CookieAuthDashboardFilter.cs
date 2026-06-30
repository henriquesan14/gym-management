using GymManagementSystem.Domain.Enums;
using Hangfire.Dashboard;
using System.Security.Claims;

namespace GymManagementSystem.API.Filters;

public sealed class CookieAuthDashboardFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        if (httpContext.User.Identity?.IsAuthenticated != true)
            return false;

        return httpContext.User.HasClaim(ClaimTypes.Role, UserRole.Admin.ToString());
    }
}
