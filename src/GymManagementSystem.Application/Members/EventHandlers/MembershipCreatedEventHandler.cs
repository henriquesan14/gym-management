using GymManagementSystem.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymManagementSystem.Application.Members.EventHandlers;

public class MembershipCreatedEventHandler : INotificationHandler<MembershipCreatedEvent>
{
    private readonly ILogger<MembershipCreatedEventHandler> _logger;

    public MembershipCreatedEventHandler(
        ILogger<MembershipCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(
        MembershipCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Membership {MembershipId} created for Member {MemberId}.",
            notification.membershipId, notification.memberId);

        return Task.CompletedTask;
    }
}
