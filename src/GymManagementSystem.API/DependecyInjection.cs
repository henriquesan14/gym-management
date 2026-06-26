using Carter;
using GymManagementSystem.API.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

namespace GymManagementSystem.API;

public static class DependencyInjection
{
    public static void ConfigureHostUrls(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsProduction())
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.UseUrls($"http://*:{port}");
        }
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration configuration)
    {
        services.AddOpenApi();
        //services.AddCorsConfig(builder.Environment);
        services.AddJsonSerializationConfig();
        //services.AddAuthConfig(configuration, builder.Environment);

        services.AddHttpContextAccessor();
        //services.AddScoped<IUserContext, UserContext>();

        //services.AddHangfireConfig(configuration);

        services.AddCarter();

        //builder.Services.AddControllers()
        // .ConfigureApiBehaviorOptions(options =>
        // {
        //     options.SuppressModelStateInvalidFilter = true;
        // });

        //services.AddExceptionHandler<CustomExceptionHandler>();

        //services.AddHealthChecks()
            //.AddNpgSql(configuration.GetConnectionString("DbConnection")!);

        //services.AddRateLimitingConfig(builder.Configuration);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app, IConfiguration configuration)
    {
        app.UseExceptionHandler(options => { });

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("Gym Management System API")
                    .WithTheme(ScalarTheme.Purple)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            });
        }

        app.UseCors("AllowSpecificOrigin");

        app.UseAuthentication();
        app.UseAuthorization();

        //app.MapControllers();

        app.MapCarter();

        //app.UseSwaggerDocs();

        //app.UseIpRateLimiting();

        //app.UseHangfireDashboardWithAuth(configuration);
        //app.UseRecurringJobs();

        //app.UseHealthChecks("/health", new HealthCheckOptions
        //{
            //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //});

        return app;
    }
}
