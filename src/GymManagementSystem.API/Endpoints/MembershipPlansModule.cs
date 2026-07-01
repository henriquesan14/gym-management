using Carter;
using FluentValidation;
using GymManagementSystem.API.Extensions;
using GymManagementSystem.Application.Members.Commands.CreateMember;
using GymManagementSystem.Application.MembershipPlans.Commands.CreateMembershipPlan;
using MediatR;

namespace GymManagementSystem.API.Endpoints;

public sealed class MembershipPlansModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/membershipPlans").RequireAuthorization();

        group.MapPost("/", Create);
    }

    private static async Task<IResult> Create(
        CreateMembershipPlanCommand command,
        IValidator<CreateMemberCommand> validator,
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
