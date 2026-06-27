using Carter;
using FluentValidation;
using GymManagementSystem.API.Extensions;
using GymManagementSystem.Application.Auth.Commands.GenerateAccessToken;
using GymManagementSystem.Application.Auth.Commands.RenewAccessToken;
using GymManagementSystem.Application.Auth.Commands.RevokeRefreshToken;
using MediatR;

namespace GymManagementSystem.API.Endpoints;

public class AuthModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/", Login);
        group.MapPost("/refresh-token", RefreshToken);
        group.MapPost("/logout", Logout);
    }

    private static async Task<IResult> Login(
        GenerateAccessTokenCommand command,
        IValidator<GenerateAccessTokenCommand> validator,
        ISender sender,
        CancellationToken ct)
    {
        var validation = await validator.ValidateRequest(command, ct);

        if (validation is not null)
            return validation;

        var result = await sender.Send(command, ct);

        return result.ToHttpResult();
    }

    private static async Task<IResult> RefreshToken(
        RenewAccessTokenCommand command,
        ISender sender,
        CancellationToken ct)
    {
        var result = await sender.Send(command, ct);

        return result.ToHttpResult();
    }

    private static async Task<IResult> Logout(
        RevokeRefreshTokenCommand command,
        ISender sender,
        CancellationToken ct)
    {
        var result = await sender.Send(command, ct);

        return result.ToHttpResult();
    }
}
