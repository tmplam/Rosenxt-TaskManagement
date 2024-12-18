﻿using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Abstractions;

public interface IEmailService
{
    Task SendRemindEmailAsync(User user, List<TaskItem> tasks, CancellationToken cancellationToken = default);

    Task SendTagEmailAsync(TaskItem task, User user, CancellationToken cancellationToken = default);
}
