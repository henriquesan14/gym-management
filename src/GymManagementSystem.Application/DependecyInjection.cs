using FluentValidation;
using GymManagementSystem.Application.Members.Commands.CreateMember;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateMemberCommandValidator>();

        return services;
    }
}
