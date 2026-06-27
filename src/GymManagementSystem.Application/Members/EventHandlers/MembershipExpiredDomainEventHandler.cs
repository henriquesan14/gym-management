using GymManagementSystem.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymManagementSystem.Application.Members.EventHandlers;

public class MembershipExpiredDomainEventHandler : INotificationHandler<MembershipExpiredDomainEvent>
{
    private readonly ILogger<MembershipExpiredDomainEventHandler> _logger;

    public MembershipExpiredDomainEventHandler(
        ILogger<MembershipExpiredDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(
        MembershipExpiredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Membership {MembershipId} expired.",
            notification.MembershipId);

        return Task.CompletedTask;
    }
}
