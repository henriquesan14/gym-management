using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Events;
using GymManagementSystem.Domain.Exceptions;
using GymManagementSystem.Domain.MembershipPlans;
using GymManagementSystem.Domain.ValueObjects;

namespace GymManagementSystem.Domain.Members;

public sealed class Member : Aggregate<MemberId>, IAuditableEntity
{
    private readonly List<Membership> _memberships = [];
    private readonly List<CheckIn> _checkIns = [];

    public string FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;

    public IReadOnlyCollection<Membership> Memberships =>
        _memberships.AsReadOnly();

    public IReadOnlyCollection<CheckIn> CheckIns => _checkIns.AsReadOnly();

    public bool HasActiveMembership => _memberships.Any(m => m.IsActive);

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
        MembershipPlan plan,
        DateOnly startDate)
    {
        if (_memberships.Any(x => x.IsActive))
            throw new DomainException(
                "Member already has an active membership.");

        var membership = new Membership(
            MembershipId.New(),
            plan.Id,
            startDate,
            startDate.AddDays(plan.DurationInDays));

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

        var checkIn = new CheckIn(CheckInId.New(), Id);
        _checkIns.Add(checkIn);

        AddDomainEvent(new MemberCheckedInDomainEvent(Id));
    }

    public void EnterMembershipGracePeriod(MembershipId membershipId)
    {
        var membership = _memberships
            .Single(x => x.Id == membershipId);

        membership.EnterGracePeriod();

        AddDomainEvent(new MembershipEnteredGracePeriodEvent(
            Id,
            membership.Id));
    }

    public Membership RenewMembership(MembershipPlan membershipPlan)
    {
        var membership = GetActiveMembership();

        if (membership is null) throw new DomainException("There are no Active Membership");

        membership.Renew(membershipPlan);

        return membership;
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

        AddDomainEvent(new MembershipExpiredEvent(Id, membership.Id));
    }

    public Membership? GetActiveMembership()
    {
        return this.Memberships.FirstOrDefault(m => m.IsActive);
    }
}
