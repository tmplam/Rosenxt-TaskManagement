using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand(
    Guid Id,
    string Title,
    string Description,
    int RemindBeforeDeadlineByMinutes,
    DateTimeOffset DueDate) : ICommand<UpdateTaskResult>;

public record UpdateTaskResult(Guid Id);