namespace SharedKernel;

public abstract class Entity : Miscellaneous
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}