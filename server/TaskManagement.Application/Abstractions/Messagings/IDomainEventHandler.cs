using MediatR;
using TaskManagement.Domain.Primitives;

namespace TaskManagement.Application.Abstractions.Messagings;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
