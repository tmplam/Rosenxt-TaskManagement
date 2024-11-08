using TaskManagement.Domain.Primitives;

namespace TaskManagement.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
