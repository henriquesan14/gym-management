using Carter;
using FluentValidation;
using GymManagementSystem.API.Extensions;
using GymManagementSystem.Application.Payments.Commands.CreatePayment;
using GymManagementSystem.Application.Users.Commands.CreateUser;
using MediatR;

namespace GymManagementSystem.API.Endpoints;

public sealed class PaymentsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payments").RequireAuthorization();

        group.MapPost("/", Create);
    }

    private static async Task<IResult> Create(
        CreatePaymentCommand command,
        IValidator<CreateUserCommand> validator,
        ISender sender,
        CancellationToken ct)
    {
        //var validation = await validator.ValidateRequest(command, ct);

        //if (validation is not null)
        //    return validation;

        var result = await sender.Send(command, ct);

        return result.ToHttpResult();
    }
}
