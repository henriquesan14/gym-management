using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GymManagementSystem.API.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        var isDevelopment = env.IsDevelopment();
        var secretKey = Encoding.ASCII.GetBytes(configuration["TokenSettings:Secret"]!);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = !isDevelopment;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            };

            x.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Cookies["access_token"];
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
        });

        //services.AddAuthorization(options =>
        //{
        //    foreach (var role in Enum.GetValues<UserRoleEnum>())
        //    {
        //        options.AddPolicy(role.ToString(), policy =>
        //            policy.RequireClaim(ClaimTypes.Role, role.ToString()));
        //    }
        //});

        return services;
    }
}
