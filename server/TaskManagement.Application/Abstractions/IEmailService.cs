using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Abstractions;

public interface IEmailService
{
    Task SendRemindEmailAsync(User user, TaskItem task, CancellationToken cancellationToken = default);

    Task SendTagEmailAsync(User user, TaskItem task, CancellationToken cancellationToken = default);
}
