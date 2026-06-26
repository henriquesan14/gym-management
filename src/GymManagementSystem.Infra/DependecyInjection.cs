using GymManagementSystem.Application.Members;
using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Infra.Data;
using GymManagementSystem.Infra.Data.Interceptors;
using GymManagementSystem.Infra.Data.Repositories;
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
        //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<GymManagementDbContext>((sp, options) =>
        {
            //options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        //Repositories
        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        //services.AddScoped<ITenantRepository, TenantRepository>();

        //Services
        //services.AddScoped<ITokenService, TokenService>();
        //services.AddScoped<ITokenCleanupService, TokenCleanupService>();
        //services.AddScoped<ISubscriptionsJobService, SubscriptionsJobService>();
        //services.AddScoped<IEmailSender, SendGridEmailSender>();
        //services.AddScoped<IEmailService, EmailService>();
        //services.AddScoped<IAsaasService, AsaasService>();
        //services.AddScoped<ICacheService, RedisCacheService>();
        //services.AddScoped<IClientRuleCheck, RuleCheckService>();
        //services.AddScoped<IUserRuleCheck, RuleCheckService>();
        //services.AddScoped<ITenantRuleCheck, RuleCheckService>();
        //services.AddScoped<ISubscriptionPlanRuleCheck, RuleCheckService>();

        //services.AddSingleton<IPasswordCheck, PasswordService>();
        //services.AddSingleton<IPasswordHash, PasswordService>();

        return services;
    }
}
