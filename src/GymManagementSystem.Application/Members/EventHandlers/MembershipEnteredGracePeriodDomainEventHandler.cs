using GymManagementSystem.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymManagementSystem.Application.Members.EventHandlers;

public class MembershipEnteredGracePeriodDomainEventHandler : INotificationHandler<MembershipExpiredDomainEvent>
{
    private readonly ILogger<MembershipEnteredGracePeriodDomainEventHandler> _logger;

    public MembershipEnteredGracePeriodDomainEventHandler(
        ILogger<MembershipEnteredGracePeriodDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(
        MembershipExpiredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Membership {MembershipId} entered grace period.",
            notification.MembershipId);

        return Task.CompletedTask;
    }
}
