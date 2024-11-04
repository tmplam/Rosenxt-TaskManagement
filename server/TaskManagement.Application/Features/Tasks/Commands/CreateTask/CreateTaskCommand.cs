using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Commands.CreateTask;

public record CreateTaskCommand(
    string Title, 
    string Description, 
    int RemindBeforeDeadlineByMinutes, 
    DateTimeOffset DueDate) : ICommand<CreateTaskResult>;

public record CreateTaskResult(Guid Id);