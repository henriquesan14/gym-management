using Carter;
using FluentValidation;
using GymManagementSystem.API.Extensions;
using GymManagementSystem.Application.Auth.GenerateAccessToken;
using MediatR;

namespace GymManagementSystem.API.Endpoints;

public class AuthModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/", Login);
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
}
