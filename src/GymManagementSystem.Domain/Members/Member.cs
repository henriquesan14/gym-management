using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Events;
using GymManagementSystem.Domain.Exceptions;
using GymManagementSystem.Domain.ValueObjects;

namespace GymManagementSystem.Domain.Members;

public class Member : Aggregate<MemberId>
{
    private readonly List<Membership> _memberships = [];

    public string FullName { get; private set; }
    public Email Email { get; private set; }

    public IReadOnlyCollection<Membership> Memberships =>
        _memberships.AsReadOnly();

    private Member()
    {
    }

    public Member(
        MemberId id,
        string fullName,
        Email email)
    {
        Id = id;
        FullName = fullName;
        Email = email;
    }

    public Membership CreateMembership(
        DateOnly startDate,
        int durationInMonths)
    {
        if (_memberships.Any(x => x.IsActive))
            throw new DomainException(
                "Member already has an active membership.");

        var membership = new Membership(
            MembershipId.Of(Guid.NewGuid()),
            startDate,
            startDate.AddMonths(durationInMonths));

        _memberships.Add(membership);

        AddDomainEvent(
            new MembershipCreatedEvent(
                Id,
                membership.Id));

        return membership;
    }

    public void CancelMembership(MembershipId membershipId)
    {
        var membership = _memberships
            .Single(x => x.Id == membershipId);

        membership.Cancel();
    }
}
