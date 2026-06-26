using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return dequeuedEvents;
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken()) throw new DomainException(rule.Message);
    }
}
