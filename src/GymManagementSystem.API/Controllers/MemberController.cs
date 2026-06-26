using FluentValidation;
using GymManagementSystem.Application.Members.Commands.CreateMember;
using GymManagementSystem.Application.Members.Queries.GetMembers;
using GymManagementSystem.Shared.Common.ResultPattern;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

[Route("api/[controller]")]
public class MemberController(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMemberCommand command, IValidator<CreateMemberCommand> validator, CancellationToken ct)
    {
        var badRequest = ValidateOrBadRequest(command, validator);
        if (badRequest != null) return badRequest;

        var result = await mediator.Send(command, ct);

        return result.Match(
            onSuccess: () => Ok(result),
            onFailure: Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var result = await mediator.Send(new GetMembersQuery(), ct);

        return result.Match(
            onSuccess: () => Ok(result.Value),
            onFailure: Problem
        );
    }
}
