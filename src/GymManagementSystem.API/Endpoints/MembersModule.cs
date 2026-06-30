using Carter;
using FluentValidation;
using GymManagementSystem.API.Extensions;
using GymManagementSystem.API.Requests;
using GymManagementSystem.Application.Members.Commands.CancelMembership;
using GymManagementSystem.Application.Members.Commands.CheckIn;
using GymManagementSystem.Application.Members.Commands.CreateMember;
using GymManagementSystem.Application.Members.Commands.CreateMembership;
using GymManagementSystem.Application.Members.Commands.DeleteMember;
using GymManagementSystem.Application.Members.Commands.RenewMembership;
using GymManagementSystem.Application.Members.Queries.GetMemberMembership;
using GymManagementSystem.Application.Members.Queries.GetMembers;
using MediatR;

namespace GymManagementSystem.API.Endpoints;

public sealed class MemberModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/members").RequireAuthorization();

        group.MapPost("/", Create);
        group.MapGet("/", Get);
        group.MapDelete("/{id}", Delete);
        group.MapPost("/{memberId}/memberships", CreateMembership);
        group.MapPost("/{memberId}/memberships/renew", RenewMembership);
        group.MapPost("/{memberId}/memberships/{membershipId}/cancel", CancelMembership);
        group.MapGet("/{memberId}/memberships/", GetMemberMembership);
        group.MapPost("/{memberId}/checkins", CheckIn);
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

    private static async Task<IResult> Delete(
        Guid id,
        ISender sender,
        CancellationToken ct)
    {
        var command = new DeleteMemberCommand(id);
        var result = await sender.Send(command, ct);

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

    private static async Task<IResult> RenewMembership(
        Guid memberId,
        RenewMembershipRequest request,
        ISender sender,
        CancellationToken ct)
    {
        var command = new RenewMembershipCommand(memberId, request.Months);

        var result = await sender.Send(command, ct);

        return result.ToHttpResult();
    }

    private static async Task<IResult> CancelMembership(
        Guid memberId,
        Guid membershipId,
        ISender sender,
        CancellationToken ct)
    {
        var command = new CancelMembershipCommand(memberId, membershipId);

        var result = await sender.Send(command, ct);

        return result.ToHttpResult();
    }

    private static async Task<IResult> GetMemberMembership(
        Guid memberId,
        ISender sender,
        CancellationToken ct)
    {
        var query = new GetMemberMembershipQuery(memberId);

        var result = await sender.Send(query, ct);

        return result.ToHttpResult();
    }

    private static async Task<IResult> CheckIn(
        Guid memberId,
        ISender sender,
        CancellationToken ct)
    {
        var query = new CheckInCommand(memberId);

        var result = await sender.Send(query, ct);

        return result.ToHttpResult();
    }
}
