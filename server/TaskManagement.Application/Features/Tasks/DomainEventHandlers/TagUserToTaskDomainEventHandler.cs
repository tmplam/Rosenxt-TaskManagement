using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.DomainEvents;

namespace TaskManagement.Application.Features.Tasks.DomainEventHandlers;

public class TagUserToTaskDomainEventHandler(
    IUserRepository _userRepository,
    ITaskRepository _taskRepository,
    IEmailService _emailService) : IDomainEventHandler<TagUserToTaskDomainEvent>
{
    public async Task Handle(TagUserToTaskDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId);
        var task = await _taskRepository.GetByIdAsync(notification.TaskId);
        if (task != null && user != null)
        {
            //await _emailService.SendTagEmailAsync(task, user, cancellationToken);
        }
    }
}
