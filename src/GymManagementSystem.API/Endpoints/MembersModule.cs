using Carter;
using FluentValidation;
using GymManagementSystem.API.Extensions;
using GymManagementSystem.API.Requests;
using GymManagementSystem.Application.Members.Commands.CreateMember;
using GymManagementSystem.Application.Members.Commands.CreateMembership;
using GymManagementSystem.Application.Members.Queries.GetMembers;
using GymManagementSystem.Domain.Members;
using MediatR;

namespace GymManagementSystem.API.Endpoints;

public class MemberModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/members");

        group.MapPost("/", Create);
        group.MapGet("/", Get);
        group.MapPost("/{memberId}/memberships", CreateMembership);
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

    private static async Task<IResult> CreateMembership(
        Guid memberId,
        CreateMembershipRequest request,
        IValidator<CreateMembershipCommand> validator,
        ISender sender,
        CancellationToken ct)
    {
        var command = new CreateMembershipCommand(memberId, request.StartDate, request.DurationInMonths);
        var validation = await validator.ValidateRequest(command, ct);

        if (validation is not null)
            return validation;

        var result = await sender.Send(command, ct);

        return result.ToHttpResult();
    }
}
