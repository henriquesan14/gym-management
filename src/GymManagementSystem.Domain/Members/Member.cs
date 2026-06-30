using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Events;
using GymManagementSystem.Domain.Exceptions;
using GymManagementSystem.Domain.ValueObjects;

namespace GymManagementSystem.Domain.Members;

public class Member : Aggregate<MemberId>, IAuditableEntity
{
    private readonly List<Membership> _memberships = [];
    private readonly List<CheckIn> _checkIns = [];

    public string FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;

    public IReadOnlyCollection<Membership> Memberships =>
        _memberships.AsReadOnly();

    public IReadOnlyCollection<CheckIn> CheckIns => _checkIns.AsReadOnly();

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

    public void CheckIn()
    {
        if (Memberships.All(x => !x.IsActive))
            throw new DomainException("Member does not have an active membership.");

        var today = DateOnly.FromDateTime(DateTime.Now);

        if (_checkIns.Any(x => DateOnly.FromDateTime(x.CheckedInAt) == today))
            throw new DomainException("Member has already checked in today.");

        var checkIn = new CheckIn(Id);
        _checkIns.Add(checkIn);

        //AddDomainEvent(new MemberCheckedInDomainEvent(Id));
    }

    public void EnterMembershipGracePeriod(MembershipId membershipId)
    {
        var membership = _memberships
            .Single(x => x.Id == membershipId);

        membership.EnterGracePeriod();

        AddDomainEvent(new MembershipEnteredGracePeriodDomainEvent(
            Id,
            membership.Id));
    }

    public void RenewMembership(int months)
    {
        var membership = GetActiveMembership();

        if (membership is null) throw new DomainException("There are no Active Membership");

        membership.Renew(months);
    }

    public void CancelMembership(MembershipId membershipId)
    {
        var membership = _memberships
            .Single(x => x.Id == membershipId);

        if (membership is null) throw new DomainException("Membership not exists");

        membership.Cancel();
    }

    public void ExpireMembership(MembershipId membershipId)
    {
        var membership = _memberships.SingleOrDefault(x => x.Id == membershipId);

        if (membership is null)
            throw new DomainException("Membership not found.");

        membership.Expire();

        AddDomainEvent(new MembershipExpiredDomainEvent(Id, membership.Id));
    }

    public Membership? GetActiveMembership()
    {
        return this.Memberships.FirstOrDefault(m => m.IsActive);
    }
}
