using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.DomainEvents;
public sealed record RemindDueTaskDomainEvent(Guid Id, User User, List<TaskItem> Tasks) : DomainEvent(Id);
