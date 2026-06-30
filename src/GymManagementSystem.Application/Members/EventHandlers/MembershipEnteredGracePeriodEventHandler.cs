using GymManagementSystem.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymManagementSystem.Application.Members.EventHandlers;

public class MembershipEnteredGracePeriodEventHandler : INotificationHandler<MembershipExpiredEvent>
{
    private readonly ILogger<MembershipEnteredGracePeriodEventHandler> _logger;

    public MembershipEnteredGracePeriodEventHandler(
        ILogger<MembershipEnteredGracePeriodEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(
        MembershipExpiredEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Membership {MembershipId} entered grace period for Member {MemberId}.",
            notification.MembershipId, notification.MemberId);

        return Task.CompletedTask;
    }
}
