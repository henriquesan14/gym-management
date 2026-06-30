using GymManagementSystem.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymManagementSystem.Application.Members.EventHandlers;

public class MembershipExpiredEventHandler : INotificationHandler<MembershipExpiredEvent>
{
    private readonly ILogger<MembershipExpiredEventHandler> _logger;

    public MembershipExpiredEventHandler(
        ILogger<MembershipExpiredEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(
        MembershipExpiredEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Membership {MembershipId} expired for Member {MemberId}.",
            notification.MembershipId, notification.MemberId);

        return Task.CompletedTask;
    }
}
