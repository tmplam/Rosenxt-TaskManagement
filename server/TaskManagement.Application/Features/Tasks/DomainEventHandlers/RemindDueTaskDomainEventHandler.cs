using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Domain.DomainEvents;

namespace TaskManagement.Application.Features.Tasks.DomainEventHandlers;

public sealed class RemindDueTaskDomainEventHandler(IEmailService _emailService) : IDomainEventHandler<RemindDueTaskDomainEvent>
{
    public async Task Handle(RemindDueTaskDomainEvent notification, CancellationToken cancellationToken)
    {
        await _emailService.SendRemindEmailAsync(notification.User, notification.Tasks);
    }
}
