using GymManagementSystem.API.Filters;
using GymManagementSystem.Application.Auth;
using GymManagementSystem.Application.Members;
using Hangfire;
using Hangfire.PostgreSql;

namespace GymManagementSystem.API.Extensions;

public static class HangfireExtensions
{
    public static IServiceCollection AddHangfireConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));

        services.AddHangfireServer();

        return services;
    }


    public static IApplicationBuilder UseHangfireDashboardWithAuth(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            //Authorization = new[] { new CookieAuthDashboardFilter() }
        });

        return app;
    }

    public static IApplicationBuilder UseRecurringJobs(this IApplicationBuilder app)
    {
        RecurringJob.AddOrUpdate<ITokenCleanupService>(
            "CleanupExpiredAndRevokedTokensAsync",
            service => service.CleanupExpiredAndRevokedTokensAsync(),
            Cron.Daily);

        RecurringJob.AddOrUpdate<IMembershipExpirationBackgroundService>(
            "ProcessMemberships",
            service => service.ProcessMemberships(new CancellationToken()),
            Cron.Daily);

        return app;
    }
}
