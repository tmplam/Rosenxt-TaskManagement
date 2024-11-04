using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Tasks.Commands.ToggleCompleteTask;

public record ToggleCompleteTaskCommand(Guid Id) : ICommand<ToggleCompleteTaskResult>;

public record ToggleCompleteTaskResult(Guid Id);