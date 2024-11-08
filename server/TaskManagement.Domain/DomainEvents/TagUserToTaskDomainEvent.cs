using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.DomainEvents;

public sealed record TagUserToTaskDomainEvent(Guid Id, Guid UserId, Guid TaskId) : DomainEvent(Id);