using Carter;
using FluentValidation;
using GymManagementSystem.API.Extensions;
using GymManagementSystem.Application.Members.Commands.CreateMember;
using GymManagementSystem.Application.Members.Queries.GetMembers;
using MediatR;

namespace GymManagementSystem.API.Endpoints;

public class MemberModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/members");

        group.MapPost("/", Create);
        group.MapGet("/", Get);
    }

    private static async Task<IResult> Create(
        CreateMemberCommand command,
        IValidator<CreateMemberCommand> validator,
        ISender sender,
        CancellationToken ct)
    {
        var validation = await validator.ValidateRequest(command, ct);

        if (validation is not null)
            return validation;

        var result = await sender.Send(command, ct);

        return result.ToHttpResult();
    }

    private static async Task<IResult> Get(
        ISender sender,
        CancellationToken ct)
    {
        var result = await sender.Send(new GetMembersQuery(), ct);

        return result.ToHttpResult();
    }
}
