using GymManagementSystem.Application.Auth;
using GymManagementSystem.Application.Members;
using GymManagementSystem.Application.MembershipPlans;
using GymManagementSystem.Application.Payments;
using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Application.Users;
using GymManagementSystem.Domain.Users.Contracts;
using GymManagementSystem.Infra.Data;
using GymManagementSystem.Infra.Data.Interceptors;
using GymManagementSystem.Infra.Data.Repositories;
using GymManagementSystem.Infra.Options;
using GymManagementSystem.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagementSystem.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<GymManagementDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        //Repositories
        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IMembershipPlanRepository, MembershipPlanRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        //Services
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITokenCleanupService, TokenCleanupService>();
        services.AddScoped<IMembershipExpirationBackgroundService, MembershipExpirationBackgroundService>();
        services.AddScoped<ICheckInService, CheckInService>();

        services.AddSingleton<IPasswordCheck, PasswordService>();
        services.AddSingleton<IPasswordHash, PasswordService>();

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.Secret),
                "JWT Secret is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.Issuer),
                "JWT Issuer is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.Audience),
                "JWT Audience is required.")
            .Validate(options => options.AccessTokenExpirationInMinutes > 0,
                "AccessTokenExpirationInMinutes must be greater than zero.")
            .Validate(options => options.RefreshTokenExpirationInDays > 0,
                "RefreshTokenExpirationInDays must be greater than zero.")
            .ValidateOnStart();

        services.Configure<IpRateLimitingOptions>(
            configuration.GetSection(IpRateLimitingOptions.SectionName));

        services.AddOptions<MembershipOptions>()
            .Bind(configuration.GetSection(MembershipOptions.SectionName))
            .Validate(options => options.GracePeriodDays > 0,
                "GracePeriodDays must be greater than zero.")
            .ValidateOnStart();


        return services;
    }
}
